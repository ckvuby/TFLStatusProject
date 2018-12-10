using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
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


        Startup()
        {
            HttpClient = new HttpClient();
            httpClientWrapper = new HttpClientWrapper(HttpClient);
            apiClass = new TFLApiClient(httpClientWrapper);
            ConsoleApp = new ConsoleApp(apiClass, new Steve(apiClass));
        }

        static void Main(string[] args)
        {
            var startup = new Startup();
            ConsoleApp.ConsoleAppHandler(args, HttpClient, httpClientWrapper);
        }
    }
}
