using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace TFLStatusLibrary.Tests
{
    public class TFLApiClientShould
    {

        [Fact]
        public async void MakeACallToApi()
        {
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";
            TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
            await tflClient.MakeTFLApiCall();
            httpClient.Verify(x => x.GetAsync(url), Times.Once());
        }

        [Fact]
        public async Task ReturnHTTPMessageResponseAsync()
        {
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is the response message")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(responseMessage));

            TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
            var response2 = tflClient.MakeTFLApiCall();

            Assert.Equal(responseMessage, response2.Result);

        }

        [Fact]
        public async Task ReturnHTTPMessageResponseAsyncWithContent()
        {
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

            // Create json

            var responseAsJson = new JArray
            {
                new JObject
                {
                    new JProperty("id", "Bakerloo"),
                    new JProperty("name", "Bakerloo"),
                    new JProperty("lineStatuses", new JArray
                    {
                        new JObject
                        {
                            new JProperty("statusSeverityDescription", "Good Service"),
                            new JProperty("reason", "")
                        }                    
                    })        
                }
            };

            var responseAsJsonTwo = new JArray
            {
                new JObject
                {
                    new JProperty("id", "Circle"),
                    new JProperty("name", "Circle"),
                    new JProperty("lineStatuses", new JArray
                    {
                        new JObject
                        {
                            new JProperty("statusSeverityDescription", "Minor Delays"),
                            new JProperty("reason", "Circle Line: MINOR DELAYS due to emergency work in the Euston Square area.")
                        }
                    })
                }
            };

            // Pass json as string
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                //Content = new StringContent(JsonConvert.SerializeObject(responseAsJson))
              // Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.Line, Tfl.Api.Presentation.Entities\",\"id\":\"circle\",\"name\":\"Circle\",\"modeName\":\"tube\",\"disruptions\":[],\"created\":\"2018-11-28T11:37:03.677Z\",\"modified\":\"2018-11-28T11:37:03.677Z\",\"lineStatuses\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineStatus, Tfl.Api.Presentation.Entities\",\"id\":0,\"lineId\":\"circle\",\"statusSeverity\":9,\"statusSeverityDescription\":\"Minor Delays\",\"reason\":\"Circle Line: MINOR DELAYS due to emergency work in the Euston Square area. \",\"created\":\"0001-01-01T00:00:00\",\"validityPeriods\":[{\"$type\":\"Tfl.Api.Presentation.Entities.ValidityPeriod, Tfl.Api.Presentation.Entities\",\"fromDate\":\"2018-12-07T11:01:48Z\",\"toDate\":\"2018-12-08T01:29:00Z\",\"isNow\":true}],\"disruption\":{\"$type\":\"Tfl.Api.Presentation.Entities.Disruption, Tfl.Api.Presentation.Entities\",\"category\":\"RealTime\",\"categoryDescription\":\"RealTime\",\"description\":\"Circle Line: MINOR DELAYS due to emergency work in the Euston Square area. \",\"affectedRoutes\":[{\"$type\":\"Tfl.Api.Presentation.Entities.RouteSection, Tfl.Api.Presentation.Entities\",\"id\":\"1633\",\"name\":\"Edgware Road (Circle Line) Underground Station - Hammersmith (H&C Line) Underground Station\",\"direction\":\"inbound\",\"originationName\":\"Edgware Road (Circle Line) Underground Station\",\"destinationName\":\"Hammersmith (H&C Line) Underground Station\",\"routeSectionNaptanEntrySequence\":[]}]")
               Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.Line, Tfl.Api.Presentation.Entities\",\"id\":\"bakerloo\",\"name\":\"Bakerloo\",\"modeName\":\"tube\",\"disruptions\":[],\"created\":\"2018-11-28T11:37:03.687Z\",\"modified\":\"2018-11-28T11:37:03.687Z\",\"lineStatuses\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineStatus, Tfl.Api.Presentation.Entities\",\"id\":0,\"statusSeverity\":10,\"statusSeverityDescription\":\"Good Service\",\"created\":\"0001-01-01T00:00:00\",\"validityPeriods\":[]}],\"routeSections\":[],\"serviceTypes\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineServiceTypeInfo, Tfl.Api.Presentation.Entities\",\"name\":\"Regular\",\"uri\":\"/Line/Route?ids=Bakerloo&serviceTypes=Regular\"}],\"crowding\":{\"$type\":\"Tfl.Api.Presentation.Entities.Crowding, Tfl.Api.Presentation.Entities\"}}]")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(responseMessage));

            // Expect List of LineInformation
            /*
            var expectedLineInformation = new List<LineInformation>
            {
                new LineInformation
                {
                    lineId = "circle",
                    lineName = "Circle",
                    lineStatus = "Minor Delays",
                    statusReason = "Circle Line: MINOR DELAYS due to emergency work in the Euston Square area."
                }
            };
               */


            var expectedLineInformation = new List<LineInformation>
         {
             new LineInformation
             {
                 lineId = "bakerloo",
                 lineName = "Bakerloo",
                 lineStatus = "Good Service",
                 statusReason = null
             }
         };



            TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
          var response = tflClient.SetupAndMakeApiCallAndReturnFormattedData().ToList();

          // Assert the passed json resolves to expected line information
          Assert.Equal(expectedLineInformation[0].lineId, response[0].lineId);
          //Assert.Equal(expectedLineInformation, response.ToList());

      }


    

      [Fact]
      public async Task WriteErrorMessageToConsoleIfBadStatusCode()
      {
          using (StringWriter sw = new StringWriter())
          {
              Console.SetOut(sw);

              Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
              var url = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

              Mock<HttpResponseMessage> responseMessage = new Mock<HttpResponseMessage>(HttpStatusCode.BadGateway);

              httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(responseMessage.Object));

              TFLApiClient tflClient = new TFLApiClient(httpClient.Object);

              var responseObject = tflClient.SetupAndMakeApiCallAndReturnFormattedData();

              string expected = string.Format("Sorry information is not available{0}", Environment.NewLine);
              Assert.Null(responseObject);
              Assert.Equal(expected, sw.ToString());

              sw.Flush();
          }

      }

      [Fact]
      public void ThrowAnExceptionWhenNoResponseFromAPI()
      {

          Mock<IHttpClient> httpClient = new Mock<IHttpClient>();

          httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).Throws(new Exception("Sorry there was an error"));

          TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
          Exception ex = Assert.Throws<AggregateException>(() => tflClient.MakeTFLApiCall().Result);

          Assert.Equal("Sorry there was an error", ex.InnerException.Message);
      }
  }
}
 