using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using CommandLine;
using TFLStatus;
using TFLStatusLibrary;

namespace TFLStatusConsoleApp
{
    public class ConsoleApp : IConsoleApp
    {
        private readonly ITFLAPIClient _apiClass;

        public IEnumerable<string> GetLine {
            get;
            set;
        }
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
                    else 
                    {
                        ShowStatusOfAllTubeLines(o.GetLine);
                    }

                });

            Console.ReadLine();
        }

        public void ShowStatusOfAllTubeLines()
        {
            var emptyString = "";
            ShowStatusOfAllTubeLines(emptyString);
        }

        public void ShowStatusOfAllTubeLines(string lineName)
        {
            var tflStatusData = _apiClass.SetupAndMakeApiCallAndReturnFormattedDataAsync().Result;
            
            foreach (LineInformation lines in tflStatusData.Where(x=> x.LineName == lineName || x.LineName.IsNullOrEmpty()))
            {
                Console.WriteLine(lines.LineName + " ------ " + lines.LineStatus + "  " + lines.StatusReason);
            }
        }
        /*
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
        */
    }
}
