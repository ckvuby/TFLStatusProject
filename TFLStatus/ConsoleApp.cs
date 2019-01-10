using System;
using CommandLine;
using TFLStatus;
using TFLStatusLibrary;

namespace TFLStatusConsoleApp
{
    public class ConsoleApp : IConsoleApp
    {
        private readonly ITFLAPIClient _apiClass;

        public ConsoleApp(ITFLAPIClient apiClass)
        {
            this._apiClass = apiClass;
        }

        public void ConsoleAppHandler(string[] args)
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
            var tflStatusData = _apiClass.SetupAndMakeApiCallAndReturnFormattedData().Result;

            foreach (LineInformation lines in tflStatusData)
            {
                Console.WriteLine(lines.LineName + " ------ " + lines.LineStatus + "  " + lines.StatusReason);
            }
        }

        public void ShowStatusOfVictoriaLine()
        {
            var tflStatusData = _apiClass.SetupAndMakeApiCallAndReturnFormattedData().Result;

            foreach (LineInformation lines in tflStatusData)
            {
                if (lines.LineName == "Victoria")
                {
                    Console.WriteLine(lines.LineName + " ------ " + lines.LineStatus + "  " + lines.StatusReason);
                }
            }
        }

    }
}
