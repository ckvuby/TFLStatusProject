using Moq;
using Moq.Protected;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
            }
        }

        [Fact]
        public async Task ThrowAnExceptionWhenNoResponseFromAPI()
        {
            using (StringWriter sw = new StringWriter())
            {
             Console.SetOut(sw);

             Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
             var url = "https://fake_uirl_test";
            
             httpClient.Setup(x => x.GetAsync(url)).Throws(new Exception());

             TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
             tflClient.SetupAndMakeApiCallAndReturnFormattedData();

             string expected = string.Format("Sorry there was an error{0}", Environment.NewLine);
             Assert.Equal(expected, sw.ToString());
            }
        }

       
    }
}