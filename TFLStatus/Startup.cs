﻿using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using TFLStatusLibrary;

namespace TFLStatus
{
    class Startup
    {
        public static ITFLAPIClient tflApiClient;
        public static HttpClient HttpClient;
        public static ConsoleApp ConsoleApp;
        public static IHttpClient httpClientWrapper;
        public Uri url;


        Startup(Uri appConfig)
        {
            url = appConfig;
            HttpClient = new HttpClient();
            httpClientWrapper = new HttpClientWrapper(HttpClient);
            tflApiClient = new TFLApiClient(httpClientWrapper, url);
            ConsoleApp = new ConsoleApp(tflApiClient);
        }


        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
              Uri appConfig = new Uri(configuration.GetSection("MySettings").GetSection("WebApiBaseUrl").Value);

            var startup = new Startup(appConfig);
            ConsoleApp.ConsoleAppHandler(args, HttpClient, httpClientWrapper);
        }
    }
}
