using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using TFLStatus;
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
            LineInformation lineInfoVictoria = new LineInformation();
            LineInformation lineInfoBakerloo = new LineInformation();
            LineInformation lineInfoCircle = new LineInformation();

            lineInfoVictoria.lineName = "Victoria";
            lineInfoVictoria.lineStatus = "Good Service";

            lineInfoBakerloo.lineName = "Bakerloo";
            lineInfoBakerloo.lineStatus = "Good Service";

            lineInfoCircle.lineName = "Circle";
            lineInfoCircle.lineStatus = "Good Service";

            List<LineInformation> mockDataOfApi = new List<LineInformation>();
            mockDataOfApi.Add(lineInfoVictoria);
            mockDataOfApi.Add(lineInfoBakerloo);
            mockDataOfApi.Add(lineInfoCircle);

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

                lineInfoVictoria.lineName = "Victoria";
                lineInfoVictoria.lineStatus = "Good Service";

                lineInfoBakerloo.lineName = "Bakerloo";
                lineInfoBakerloo.lineStatus = "Good Service";

                lineInfoCircle.lineName = "Circle";
                lineInfoCircle.lineStatus = "Good Service";

                List<LineInformation> mockDataOfApi = new List<LineInformation>();
                mockDataOfApi.Add(lineInfoVictoria);
                mockDataOfApi.Add(lineInfoBakerloo);
                mockDataOfApi.Add(lineInfoCircle);

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


    }

}

