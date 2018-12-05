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
        public async Task MakeTFLApiCallAsync()
        {
            var fakeResponseHandler = new MockResponseHandler();
            fakeResponseHandler.AddFakeResponse(new Uri("https://api.tfl.gov.uk/line/mode/tube/status?detail=true"), new HttpResponseMessage(HttpStatusCode.OK));

            var httpClient = new HttpClient(fakeResponseHandler);

            var response1 = await httpClient.GetAsync("http://example.org/notthere");
            var response2 = await httpClient.GetAsync("https://api.tfl.gov.uk/line/mode/tube/status?detail=true");

            Assert.Equal(response1.StatusCode, HttpStatusCode.NotFound);
            Assert.Equal(response2.StatusCode, HttpStatusCode.OK);

        }

        [Fact]
        public void MapResponseStringToObject()
        {

        }
    }
}
