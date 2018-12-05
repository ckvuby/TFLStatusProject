using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Moq;
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
                string expected = string.Format("Welcome to TFL Status update \nPlease pick an option from below{0}", Environment.NewLine);

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
                consoleApp.DisplayOptions();
                string expected = string.Format("[1] - Status of all tube lines{0}", Environment.NewLine);
              
                Assert.Equal(expected, sw.ToString());
            }
        }

        [Fact]
        public void DisplayTheStatusOfAllTubeLinesIfUserChoosesOptionOne()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                var consoleApp = new ConsoleApp();
                List<Hashtable> mockTflStatusData = new List<Hashtable>();

                Hashtable hashTableVictoria = new Hashtable();
                hashTableVictoria.Add("LineName", "Victoria");
                hashTableVictoria.Add("LineStatus", "Good Service");

                Hashtable hashTableBakerloo = new Hashtable();
                hashTableBakerloo.Add("LineName", "Bakerloo");
                hashTableBakerloo.Add("LineStatus", "Good Service");

                Hashtable hashTableCircle = new Hashtable();
                hashTableCircle.Add("LineName", "Circle");
                hashTableCircle.Add("LineStatus", "Good Service");

                mockTflStatusData.Add(hashTableVictoria);
                mockTflStatusData.Add(hashTableBakerloo);
                mockTflStatusData.Add(hashTableCircle);

                Console.SetOut(sw);

                var input = new StringReader("1");
                Console.SetIn(input);

                Mock<IMockApiClass> mockClass = new Mock<IMockApiClass>();
                mockClass.Setup(x => x.GetDataFromApi()).Returns(mockTflStatusData);

                // Act
                consoleApp.GetUserInput(mockClass.Object);
                string expected = string.Format("Victoria ------ Good Service{0}Bakerloo ------ Good Service{0}Circle ------ Good Service{0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
            }

        }
    }
    
}

