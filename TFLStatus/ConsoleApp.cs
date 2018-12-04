using System;

namespace TFLStatus
{
    public class ConsoleApp
    {
        static void Main(string[] args)
        {
            Greeter();
            DisplayAllLines();
        }

        public static void Greeter()
        {
            Console.WriteLine("Welcome to TFL Status update \nPlease pick an option from below");
        }

        public static void DisplayAllLines()
        {
            Console.WriteLine("[1] - Status of all tube lines");
        }
    }
}
