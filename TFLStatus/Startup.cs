using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
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


        Startup(AppConfig thing)
        {
            url = thing.WebApiBaseUrl;

            HttpClient = new HttpClient();
            httpClientWrapper = new HttpClientWrapper(HttpClient);
            apiClass = new TFLApiClient(httpClientWrapper, url);
            ConsoleApp = new ConsoleApp(apiClass);
        }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            var thing = new AppConfig();
            configuration.GetSection("MySettings").Bind(thing);

            //string Url = configuration["WebApiBaseUrl"];
            //Console.WriteLine(thing.WebApiBaseUrl);

            var startup = new Startup(thing);
            ConsoleApp.ConsoleAppHandler(args, HttpClient, httpClientWrapper);
        }
    }
}
