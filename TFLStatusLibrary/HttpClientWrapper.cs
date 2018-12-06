using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{
        public class HttpClientWrapper : IHttpClient
        {
            private readonly HttpClient _httpClient;

            public HttpClientWrapper(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

        //public HttpRequestHeaders DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

      
        public Task<HttpResponseMessage> GetAsync(string requestUri)
            {
                return _httpClient.GetAsync(requestUri);
            }

        
            public void SetHeaders()
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
         
    }
          
   
    
}
