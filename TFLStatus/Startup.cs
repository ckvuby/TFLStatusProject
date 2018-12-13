using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Castle.Core.Configuration;
using CommandLine;
using TFLStatusLibrary;

namespace TFLStatus
{
    class Startup
    {
        public static ITFLAPIClient apiClass;
        public static HttpClient HttpClient;
        public static ConsoleApp ConsoleApp;
        public static IHttpClient httpClientWrapper;
        public Uri url;


        Startup()
        {
            url = new Uri("https://api.tfl.gov.uk/line/mode/tube/status?detail=true");
         
            HttpClient = new HttpClient();
            httpClientWrapper = new HttpClientWrapper(HttpClient);
            apiClass = new TFLApiClient(httpClientWrapper, url);
            ConsoleApp = new ConsoleApp(apiClass);
        }

        static void Main(string[] args)
        {
            var startup = new Startup();
            ConsoleApp.ConsoleAppHandler(args, HttpClient, httpClientWrapper);
        }
    }
}
