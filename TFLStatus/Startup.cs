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

        // TODO: Why static?
        // TODO: Why module level variables?
        public static HttpClient HttpClient;
        public static ConsoleApp ConsoleApp;
        public static IHttpClient HttpClientWrapper;
        public Uri Url;


        Startup(Uri appConfig)
        {

            Url = appConfig;
            HttpClient = new HttpClient();
            HttpClientWrapper = new HttpClientWrapper(HttpClient);
            TflApiClient = new TflApiClient(HttpClientWrapper, Url);
            ConsoleApp = new ConsoleApp(TflApiClient);

        }


        // TODO: SRP broken, config loaded, and uri fetched and console app initialised
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
              Uri appConfig = new Uri(configuration.GetSection("MySettings").GetSection("WebApiBaseUrl").Value);

            var startup = new Startup(appConfig);
            ConsoleApp.ConsoleAppHandler(args);
        }
    }
}
