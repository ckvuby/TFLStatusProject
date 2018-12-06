using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TFLStatusLibrary
{



    public interface IHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        void SetHeaders();

    }

}
