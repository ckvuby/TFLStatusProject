using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using TFLStatusConsoleApp;
using TFLStatusLibrary;

namespace TFLStatus
{
    class Startup
    {
        public static ITFLAPIClient TflApiClient;
        public static HttpClient HttpClient;
        public static ConsoleApp ConsoleApp;
        public static IHttpClient HttpClientWrapper;
        private readonly Uri Url;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
              Uri appConfigUrl = new Uri(configuration.GetSection("MySettings").GetSection("WebApiBaseUrl").Value);

            //var startup = new Startup(appConfig);
            CreateInstancesRequired(appConfigUrl);
            ConsoleApp.ConsoleAppHandler(args);
        }

        public static void CreateInstancesRequired(Uri AppConfigUrl)
        {
            var Url = AppConfigUrl;
            HttpClient = new HttpClient();
            HttpClientWrapper = new HttpClientWrapper(HttpClient);
            TflApiClient = new TflApiClient(HttpClientWrapper, Url);
            ConsoleApp = new ConsoleApp(TflApiClient);
        }
    }
}
