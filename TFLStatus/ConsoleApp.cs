using System;
using System.Collections;
using System.Collections.Generic;
using CommandLine;


namespace TFLStatus
{
    public class ConsoleApp : IConsoleApp
    {
        public static IMockApiClass MockClass;

        public ConsoleApp()
        {
            MockClass = new MockApiClass();
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.AllTubeLineStatus)
                    {
                        var consoleApp = new ConsoleApp();
                        consoleApp.ShowStatusOfAllTubeLines(o, MockClass);
                    }else
                    {
                        Console.WriteLine("Sorry no valid parameter");
                    }
                });

            Console.ReadLine();
        }

        public void ShowStatusOfAllTubeLines(Options options, IMockApiClass apiClass)
        {
            var tflStatusData = apiClass.GetDataFromApi();

            foreach (Hashtable lines in tflStatusData)
            {
                Console.WriteLine(lines["LineName"] + " ------ " + lines["LineStatus"]);
            }
        }
    }
}
