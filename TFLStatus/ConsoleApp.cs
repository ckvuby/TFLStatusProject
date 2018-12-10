using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using CommandLine;
using TFLStatusLibrary;


namespace TFLStatus
{
    public class ConsoleApp : IConsoleApp
    {
        public ITFLAPIClient apiClass;
        private ISteve _Steve;

        public ConsoleApp(ITFLAPIClient apiClass, ISteve steve)
        {
            this.apiClass = apiClass;
            this._Steve = steve;
        }

        public void ConsoleAppHandler(string[] args, HttpClient httpClient, IHttpClient httpClientWrapper)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.AllTubeLineStatus)
                    {
                        _Steve.ShowStatusOfAllTubeLines(o, httpClient, httpClientWrapper);
                    }
                    else
                    {
                        Console.WriteLine("Sorry no valid parameter");
                    }
                });

            Console.ReadLine();
        }

        
    }

    public interface ISteve
    {
        void ShowStatusOfAllTubeLines(Options options, HttpClient httpClient, IHttpClient httpClientWrapper);
    }

    public class Steve : ISteve
    {
        private readonly ITFLAPIClient _tflapiClient;

        public Steve(ITFLAPIClient tflapiClient)
        {
            _tflapiClient = tflapiClient;

        }

        public void ShowStatusOfAllTubeLines(Options options, HttpClient httpClient, IHttpClient httpClientWrapper)
        {
            var tflStatusData = _tflapiClient.SetupAndMakeApiCallAndReturnFormattedData();

            foreach (LineInformation lines in tflStatusData)
            {
                Console.WriteLine(lines.lineName + " ------ " + lines.lineStatus + "  " + lines.statusReason);
            }
        }
    }
}
