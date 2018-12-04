using System;
using System.IO;
using TFLStatus;
using Xunit;

namespace ConsoleAppTests
{
    public class ConsoleAppShould
    {
        [Fact]
        public void GreetWithAWelcomeMessage()
        {
            using (StringWriter sw = new StringWriter())
            {
                var consoleApp = new ConsoleApp();
                Console.SetOut(sw);
                consoleApp.Greeter();
                string expected = string.Format("Welcome to TFL Status update \nPlease pick an option from be{0}", Environment.NewLine);

                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]

        public void DisplayOptionForAllLineStatus()
        {
            using (StringWriter sw = new StringWriter())
            {
                var consoleApp = new ConsoleApp();
                Console.SetOut(sw);
                consoleApp.DisplayAllLines();
                string expected = string.Format("[1] - Status of all tube lines{0}", Environment.NewLine);
              
                Assert.Equal(expected, sw.ToString());
            }
        }

    }
    
}

