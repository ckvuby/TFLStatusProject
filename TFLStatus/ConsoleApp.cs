using System;

namespace TFLStatus
{
    public class ConsoleApp : IConsoleApp
    {
        static void Main(string[] args)
        {
            var consoleapp = new ConsoleApp();
            consoleapp.Greeter();
            consoleapp.DisplayAllLines();
        }

        public void Greeter()
        {
            Console.WriteLine("Welcome to TFL Status update \nPlease pick an option from be");
        }

        public void DisplayAllLines()
        {
            Console.WriteLine("[1] - Status of all tube lines");
        }
    }
}
