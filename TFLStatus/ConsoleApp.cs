using System;
using System.Net.Http;
using CommandLine;
using TFLStatusLibrary;


namespace TFLStatus
{
    public class ConsoleApp : IConsoleApp
    {
        public ITFLAPIClient apiClass;

        public ConsoleApp(ITFLAPIClient apiClass)
        {
            this.apiClass = apiClass;
        }

        public void ConsoleAppHandler(string[] args, HttpClient httpClient, IHttpClient httpClientWrapper)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.AllTubeLineStatus)
                    {
                        ShowStatusOfAllTubeLines();
                    }
                    else if (o.VictoriaTubeLineStatus)
                    {
                        ShowStatusOfVictoriaLine();
                    }
                    else
                    {
                        Console.WriteLine("Sorry no valid parameter");
                    }
                });

            Console.ReadLine();
        }

        public void ShowStatusOfAllTubeLines()
        {
            var tflStatusData = apiClass.SetupAndMakeApiCallAndReturnFormattedData();

            foreach (LineInformation lines in tflStatusData)
            {
                Console.WriteLine(lines.lineName + " ------ " + lines.lineStatus + "  " + lines.statusReason);
            }
        }

        public void ShowStatusOfVictoriaLine()
        {
            var tflStatusData = apiClass.SetupAndMakeApiCallAndReturnFormattedData();

            foreach (LineInformation lines in tflStatusData)
            {
                if (lines.lineId == "victoria")
                {
                    Console.WriteLine(lines.lineName + " ------ " + lines.lineStatus + "  " + lines.statusReason);
                }
            }
        }
    }
}
