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
    public class ConsoleAppShould : IDisposable
    {
        private readonly Mock<ITFLAPIClient> mockOfApi;
        private readonly List<LineInformation> mockDataOfApi;
        private readonly StringWriter sw;

        public ConsoleAppShould()
        {
            sw = new StringWriter();
            Console.SetOut(sw);
            LineInformation lineInfoVictoria = new LineInformation();
            LineInformation lineInfoBakerloo = new LineInformation();
            LineInformation lineInfoCircle = new LineInformation();

            lineInfoVictoria.LineName = "Victoria";
            lineInfoVictoria.LineStatus = "Good Service";

            lineInfoBakerloo.LineName = "Bakerloo";
            lineInfoBakerloo.LineStatus = "Good Service";

            lineInfoCircle.LineName = "Circle";
            lineInfoCircle.LineStatus = "Good Service";

             mockDataOfApi = new List<LineInformation>
            {
                lineInfoVictoria, lineInfoBakerloo, lineInfoCircle
            };

             mockOfApi = new Mock<ITFLAPIClient>();

        }

        public void Dispose()
        {
            sw.Dispose();
        }

        [Fact]
        public void CheckCallIsMadeToClientApi()
        {
            // Arrange
           
            mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
            var consoleApp = new ConsoleApp(mockOfApi.Object);
           
            // Act
            consoleApp.ShowStatusOfAllTubeLines();

            // Assert
            mockOfApi.Verify(m => m.SetupAndMakeApiCallAndReturnFormattedData(), Times.Once());
        }

        
        [Fact]
        public void CheckCallIsMadeToClientApiForVictoriaLine()
        {
            mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
            var consoleApp = new ConsoleApp(mockOfApi.Object);

            //Act
            consoleApp.ShowStatusOfVictoriaLine();

            //Assert
            mockOfApi.Verify(m => m.SetupAndMakeApiCallAndReturnFormattedData(), Times.Once());
        }
        

        [Fact]
        public void DisplayTheStatusOfAllTubeLines()
        {

                // Arrange        
                mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
                var consoleApp = new ConsoleApp(mockOfApi.Object);    
                
                // Act
                consoleApp.ShowStatusOfAllTubeLines();
                string expected = string.Format("Victoria ------ Good Service  {0}Bakerloo ------ Good Service  {0}Circle ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
                
            

        }

        [Fact]
        public void DisplayTheStatusOfVictoriaLine()
        {
          
                // Arrange       
                mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
                var consoleApp = new ConsoleApp(mockOfApi.Object);
               // Console.SetOut(sw);

                // Act
                consoleApp.ShowStatusOfVictoriaLine();
                //sw.Close();
                string expected = string.Format("Victoria ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
   
        }

       

    }

}

