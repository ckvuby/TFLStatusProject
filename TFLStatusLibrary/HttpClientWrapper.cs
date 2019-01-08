using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
    /// <summary>
    /// TODO: Best practice is that your interfaces are in a separate namespace to the implementation
    /// </summary>
        public class HttpClientWrapper : IHttpClient
        {
            private readonly HttpClient _httpClient;

            public HttpClientWrapper(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public Task<HttpResponseMessage> GetAsync(Uri requestUri)
            {
                return _httpClient.GetAsync(requestUri);
            }

            /// <summary>
            /// TODO: Why is this public?
            /// TODO: Ensure _httpClient is set before execution (watch out for nulls)
            /// </summary>
            public void SetHeaders()
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
         
    }
}
