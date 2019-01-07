using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TFLStatusLibrary.Tests
{
    public class TFLApiClientShould
    {

        [Fact]
        public async void MakeACallToApi()
        {
            // Arrange
            var url = new Uri("https://www.thisisfake.com");
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            ITFLAPIClient tflClient = new TflApiClient(httpClient.Object, url);

            // Act
            await tflClient.MakeTFLApiCallAsync();

            // Assert
            httpClient.Verify(x => x.GetAsync(url), Times.Once());
        }

        [Fact]
        public async Task ReturnHttpMessageResponseAsync()
        {
            // Arrange
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = new Uri("https://www.thisisfake.com");
            var expected = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is the response message")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult(expected));

            ITFLAPIClient tflClient = new TflApiClient(httpClient.Object, url);


            // Act
            var actual = await tflClient.MakeTFLApiCallAsync();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ReturnHttpMessageResponseAsyncWithContent()
        {
            // Arrange
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = new Uri("https://www.thisisfake.com");
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
               Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.Line, Tfl.Api.Presentation.Entities\",\"id\":\"bakerloo\",\"name\":\"Bakerloo\",\"modeName\":\"tube\",\"disruptions\":[],\"created\":\"2018-11-28T11:37:03.687Z\",\"modified\":\"2018-11-28T11:37:03.687Z\",\"lineStatuses\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineStatus, Tfl.Api.Presentation.Entities\",\"id\":0,\"statusSeverity\":10,\"statusSeverityDescription\":\"Good Service\",\"created\":\"0001-01-01T00:00:00\",\"validityPeriods\":[]}],\"routeSections\":[],\"serviceTypes\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineServiceTypeInfo, Tfl.Api.Presentation.Entities\",\"name\":\"Regular\",\"uri\":\"/Line/Route?ids=Bakerloo&serviceTypes=Regular\"}],\"crowding\":{\"$type\":\"Tfl.Api.Presentation.Entities.Crowding, Tfl.Api.Presentation.Entities\"}}]")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult(responseMessage));
            
            var expectedLineInformation = new List<LineInformation>
                {
                    new LineInformation
                        {
                         LineId = "bakerloo",
                         LineName = "Bakerloo",
                         LineStatus = "Good Service",
                         StatusReason = null
                         }
                };

            TflApiClient tflClient = new TflApiClient(httpClient.Object, url);

            // Act
            var response = tflClient.SetupAndMakeApiCallAndReturnFormattedData().ToList();

            // Assert
            Assert.Equal(expectedLineInformation[0].LineId, response[0].LineId);
            Assert.Equal(expectedLineInformation[0].LineName, response[0].LineName);
            Assert.Equal(expectedLineInformation[0].LineStatus, response[0].LineStatus);
            Assert.Equal(expectedLineInformation[0].StatusReason, response[0].StatusReason);
        
        }


        [Fact]
      public void WriteErrorMessageToConsoleIfBadStatusCode()
      {
          using (StringWriter sw = new StringWriter())
          {
              // Arrange
              Console.SetOut(sw);
              Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
              var url = new Uri("https://www.thisisfake.com");

              Mock<HttpResponseMessage> expectedMock = new Mock<HttpResponseMessage>(HttpStatusCode.BadGateway);

              httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(expectedMock.Object));

              TflApiClient tflClient = new TflApiClient(httpClient.Object, url);

              // Act
              var actual = tflClient.SetupAndMakeApiCallAndReturnFormattedData();

              string expected = string.Format("Sorry information is not available{0}", Environment.NewLine);

              // Assert
              Assert.Null(actual);
              Assert.Equal(expected, sw.ToString());
              sw.Flush();
          }

      }

      [Fact]
      public void ThrowAnExceptionWhenNoResponseFromApi()
      {
            // Arrange
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = new Uri("https://www.thisisfake.com");

            httpClient.Setup(x => x.GetAsync(It.IsAny<Uri>())).Throws(new Exception("Sorry there was an error"));

            TflApiClient tflClient = new TflApiClient(httpClient.Object, url);

            //Act
            Exception ex = Assert.Throws<AggregateException>(() => tflClient.MakeTFLApiCallAsync().Result);

            // Assert
            Assert.Equal("Sorry there was an error", ex.InnerException.Message);
      }
  }
}
 