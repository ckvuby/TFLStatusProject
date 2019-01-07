using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using TFLStatus;
using TFLStatusConsoleApp;
using TFLStatusLibrary;
using Xunit;

namespace ConsoleAppTests
{
    public class ConsoleAppShould
    {

        [Fact]
        public void CheckCallIsMadeToClientApi()
        {
            // Arrange
            var mockDataOfApi = new List<LineInformation>();
            
            Mock<ITFLAPIClient> mockOfApi = new Mock<ITFLAPIClient>();
            mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
            var consoleApp = new ConsoleApp(mockOfApi.Object);
           
            // Act
            consoleApp.ShowStatusOfAllTubeLines();

            // Assert
            mockOfApi.Verify(m => m.SetupAndMakeApiCallAndReturnFormattedData(), Times.Once());
        }

        [Fact]
        public void DisplayTheStatusOfAllTubeLines()
        {

            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                LineInformation lineInfoVictoria = new LineInformation();
                LineInformation lineInfoBakerloo = new LineInformation();
                LineInformation lineInfoCircle = new LineInformation();

                lineInfoVictoria.LineName = "Victoria";
                lineInfoVictoria.LineStatus = "Good Service";

                lineInfoBakerloo.LineName = "Bakerloo";
                lineInfoBakerloo.LineStatus = "Good Service";

                lineInfoCircle.LineName = "Circle";
                lineInfoCircle.LineStatus = "Good Service";

                List<LineInformation> mockDataOfApi = new List<LineInformation>
                {
                    lineInfoVictoria, lineInfoBakerloo, lineInfoCircle
                };

                Mock<ITFLAPIClient> mockOfApi = new Mock<ITFLAPIClient>();
                mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
                var consoleApp = new ConsoleApp(mockOfApi.Object);    
                Console.SetOut(sw);

                // Act
                consoleApp.ShowStatusOfAllTubeLines();
                sw.Close();
                string expected = string.Format("Victoria ------ Good Service  {0}Bakerloo ------ Good Service  {0}Circle ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
            }

        }

        [Fact]
        public void DisplayTheStatusOfVictoriaLine()
        {
            using (StringWriter sw = new StringWriter())
            {
                // Arrange
                LineInformation lineInfoVictoria = new LineInformation();
                LineInformation lineInfoBakerloo = new LineInformation();
                LineInformation lineInfoCircle = new LineInformation();

                lineInfoVictoria.LineName = "Victoria";
                lineInfoVictoria.LineStatus = "Good Service";

                lineInfoBakerloo.LineName = "Bakerloo";
                lineInfoBakerloo.LineStatus = "Good Service";

                lineInfoCircle.LineName = "Circle";
                lineInfoCircle.LineStatus = "Good Service";

                List<LineInformation> mockDataOfApi = new List<LineInformation>
                {
                    lineInfoVictoria, lineInfoBakerloo, lineInfoCircle
                };

                Mock<ITFLAPIClient> mockOfApi = new Mock<ITFLAPIClient>();
                mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
                var consoleApp = new ConsoleApp(mockOfApi.Object);
                Console.SetOut(sw);

                // Act
                consoleApp.ShowStatusOfVictoriaLine();
                sw.Close();
                string expected = string.Format("Victoria ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
            }

        }

    }

}

