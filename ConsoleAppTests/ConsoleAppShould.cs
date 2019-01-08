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
    // TODO: Naming convention - should vs check
    public class ConsoleAppShould : IDisposable
    {
        // TODO: Bad style, module variables are suspicious
        private readonly Mock<ITFLAPIClient> mockOfApi;
        private readonly List<LineInformation> mockDataOfApi;

        // TODO: Definitely bad, stringwrite is disposable which means it holds on to resources
        private readonly StringWriter sw;

        // TODO: SRP broken
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
        
        /// <summary>
        /// TODO: Assertion not mentioned in test name
        /// </summary>
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

        // TODO: Test assertion verifies the name of the service, the dots and the status but this isnt mentioned in the name
        [Fact]
        public void DisplayTheStatusOfVictoriaLine()
        {
          
                // Arrange       
                mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(mockDataOfApi);
                var consoleApp = new ConsoleApp(mockOfApi.Object);

                // Act
                consoleApp.ShowStatusOfVictoriaLine();
                string expected = string.Format("Victoria ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
   
        }

       

    }

}

