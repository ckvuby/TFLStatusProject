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
    public class TflApiClientShould
    {
        private readonly Mock<IHttpClient> _httpClient;
        private readonly ITFLAPIClient _tflClient;
        private readonly Uri _url;

        public TflApiClientShould()
        {
            _url = new Uri("https://www.thisisfake.com");
            _httpClient = new Mock<IHttpClient>();
            _tflClient = new TflApiClient(_httpClient.Object, _url);
        }

        [Fact]
        public async void MakeACallToApi()
        {
            // Arrange

           

            // Act
            await _tflClient.MakeTFLApiCallAsync();

            // Assert
            _httpClient.Verify(x => x.GetAsync(_url), Times.Once());
        }

        [Fact]
        public async Task ReturnHttpMessageResponseAsync()
        {
            // Arrange
           
            var expected = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is the response message")
            };


            _httpClient.Setup(x => x.GetAsync(_url)).Returns(Task.FromResult(expected));

            // Act
            var actual = await _tflClient.MakeTFLApiCallAsync();


            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ReturnHttpMessageResponseAsyncWithContent()
        {
            // Arrange
           
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
               Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.Line, Tfl.Api.Presentation.Entities\",\"id\":\"bakerloo\",\"name\":\"Bakerloo\",\"modeName\":\"tube\",\"disruptions\":[],\"created\":\"2018-11-28T11:37:03.687Z\",\"modified\":\"2018-11-28T11:37:03.687Z\",\"lineStatuses\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineStatus, Tfl.Api.Presentation.Entities\",\"id\":0,\"statusSeverity\":10,\"statusSeverityDescription\":\"Good Service\",\"created\":\"0001-01-01T00:00:00\",\"validityPeriods\":[]}],\"routeSections\":[],\"serviceTypes\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineServiceTypeInfo, Tfl.Api.Presentation.Entities\",\"name\":\"Regular\",\"uri\":\"/Line/Route?ids=Bakerloo&serviceTypes=Regular\"}],\"crowding\":{\"$type\":\"Tfl.Api.Presentation.Entities.Crowding, Tfl.Api.Presentation.Entities\"}}]")
            };

            _httpClient.Setup(x => x.GetAsync(_url)).Returns(Task.FromResult(responseMessage));
            
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

          
            // Act
            var response = _tflClient.SetupAndMakeApiCallAndReturnFormattedData().Result.ToList();

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

              Mock<HttpResponseMessage> expectedMock = new Mock<HttpResponseMessage>(HttpStatusCode.BadGateway);

              _httpClient.Setup(x => x.GetAsync(_url)).Returns(Task.FromResult<HttpResponseMessage>(expectedMock.Object));
 
              // Act
              var actual = _tflClient.SetupAndMakeApiCallAndReturnFormattedData().Result;

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
         
            _httpClient.Setup(x => x.GetAsync(It.IsAny<Uri>())).Throws(new Exception("Sorry there was an error"));

            //Act
            Exception ex = Assert.Throws<AggregateException>(() => _tflClient.MakeTFLApiCallAsync().Result);

            // Assert
            Assert.Equal("Sorry there was an error", ex.InnerException.Message);
      }
  }
}
 