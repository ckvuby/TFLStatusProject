using System;
using System.Collections;
using System.Collections.Generic;
using CommandLine;


namespace TFLStatus
{
    public class ConsoleApp : IConsoleApp
    {
        public static IMockApiClass MockClass;

        public static List<Hashtable> MockTflStatusData { get; private set; }

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
                        ShowStatusOfAllTubeLines(o, MockClass);
                    }else
                    {
                        Console.WriteLine("Sorry no valid parameter");
                    }
                });


            Console.ReadLine();

            // var consoleapp = new ConsoleApp();

            // consoleapp.Greeter();
            // consoleapp.DisplayOptions();

        }

        public void Greeter()
        {
            Console.WriteLine("Welcome to TFL Status update \nPlease pick an option from below");
        }

        public void DisplayOptions()
        {
            Console.WriteLine("[1] - Status of all tube lines");
        }

        public void GetUserInput(IMockApiClass MockClass)
        {
            string input = Console.ReadLine();

            if (input == "1")
            {
                var apiData = MockClass.GetDataFromApi();
                DisplayStatusOfAll(apiData);
            }
        }

        public static void DisplayStatusOfAll(List<Hashtable> tflData)
        {
            foreach (Hashtable lines in tflData)
            {
                Console.WriteLine(lines["LineName"] + " ------ " + lines["LineStatus"]);
            }
        }

        static void ShowStatusOfAllTubeLines(Options options, IMockApiClass mockClass)
        {
            MockTflStatusData = mockClass.GetDataFromApi();

            foreach (Hashtable lines in MockTflStatusData)
            {
                Console.WriteLine(lines["LineName"] + " ------ " + lines["LineStatus"]);
            }
        }
    }
}
