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
                Console.SetOut(sw);
                ConsoleApp.Greeter();
                string expected = string.Format("Welcome to TFL Status update{0}", Environment.NewLine);

                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]

        public void DisplayOptionForAllLineStatus()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ConsoleApp.DisplayAllLines();
                string expected = string.Format("[1] - Status of all tube lines{0}", Environment.NewLine);
              
                Assert.Equal(expected, sw.ToString());
            }
        }

    }
    
}

