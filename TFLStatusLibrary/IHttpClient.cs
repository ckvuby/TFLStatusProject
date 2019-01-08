using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace TFLStatusLibrary
{
    /// <summary>
    /// TODO: Best practice is interfaces in different namespace to implementation
    /// </summary>
    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(Uri requestUri);
        void SetHeaders();
    }

}
