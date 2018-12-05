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
        public void DisplayTheStatusOfAllTubeLines()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                var consoleApp = new ConsoleApp();
                var options = new Options();
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

                Mock<IMockApiClass> mockClass = new Mock<IMockApiClass>();
                mockClass.Setup(x => x.GetDataFromApi()).Returns(mockTflStatusData);

                // Act
                consoleApp.ShowStatusOfAllTubeLines(options, mockClass.Object);
                string expected = string.Format("Victoria ------ Good Service{0}Bakerloo ------ Good Service{0}Circle ------ Good Service{0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
            }

        }
    }
    
}

