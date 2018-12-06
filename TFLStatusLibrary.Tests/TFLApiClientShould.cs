using Moq;
using Moq.Protected;
using System;
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
        public async Task makeApiCallandReturnHTTPMessageResponseAsync()
        {
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();

            var url = "https://api.tfl.gov.uk/line/mode/tube/status?detail=true";

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("This is the response message")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(responseMessage));

             TFLApiClient tflClient = new TFLApiClient(httpClient.Object);
            var response2 =   tflClient.MakeTFLApiCall();

           // var response2 = await httpClient.GetAsync("https://api.tfl.gov.uk/line/mode/tube/status?detail=true");

            Assert.Equal(responseMessage, response2.Result);

        }
    }
}