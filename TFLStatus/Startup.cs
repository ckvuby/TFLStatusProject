using System;
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

        Startup(AppConfig appConfig)
        {
            url = appConfig.WebApiBaseUrl;
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
            var appConfig = new AppConfig();
            configuration.GetSection("MySettings").Bind(appConfig);

            var startup = new Startup(appConfig);
            ConsoleApp.ConsoleAppHandler(args, HttpClient, httpClientWrapper);
        }
    }
}
