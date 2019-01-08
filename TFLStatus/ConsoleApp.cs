using System;
using CommandLine;
using TFLStatus;
using TFLStatusLibrary;

namespace TFLStatusConsoleApp
{
    public class ConsoleApp : IConsoleApp
    {
        // TODO: Naming, all caps, why?
        // TODO: Naming, _apiClass should not include suffix class as its an object
        private readonly ITFLAPIClient _apiClass;

        public ConsoleApp(ITFLAPIClient apiClass)
        {
            this._apiClass = apiClass;
        }

        // TODO: Naming, what is an apphandler?
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

        // TODO: Exception handling
        public void ShowStatusOfAllTubeLines()
        {
            // TODO: Naming, function name indicates SRP broken
            // TODO: Naming, data is ambiuous, its a list of lines so should probably include lines in its name
            var tflStatusData = _apiClass.SetupAndMakeApiCallAndReturnFormattedData();

            // TODO: Use VAR rather than type
            foreach (LineInformation lines in tflStatusData)
            {
                Console.WriteLine(lines.LineName + " ------ " + lines.LineStatus + "  " + lines.StatusReason);
            }
        }

        // TODO: Exception handling
        // TODO: Suspicious, we're retrieving ALL lines then only showing 1, cannot we filter beforehand?
        public void ShowStatusOfVictoriaLine()
        {
            var tflStatusData = _apiClass.SetupAndMakeApiCallAndReturnFormattedData();

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
