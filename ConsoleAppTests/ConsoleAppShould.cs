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
        private readonly Mock<ITFLAPIClient> _mockOfApi;
        private readonly List<LineInformation> _mockDataOfApi;
        private readonly StringWriter _sw;

        public ConsoleAppShould()
        {
            _sw = new StringWriter();
            Console.SetOut(_sw);
            LineInformation lineInfoVictoria = new LineInformation();
            LineInformation lineInfoBakerloo = new LineInformation();
            LineInformation lineInfoCircle = new LineInformation();

            lineInfoVictoria.LineName = "Victoria";
            lineInfoVictoria.LineStatus = "Good Service";

            lineInfoBakerloo.LineName = "Bakerloo";
            lineInfoBakerloo.LineStatus = "Good Service";

            lineInfoCircle.LineName = "Circle";
            lineInfoCircle.LineStatus = "Good Service";


             _mockDataOfApi = new List<LineInformation>
            {
                lineInfoVictoria, lineInfoBakerloo, lineInfoCircle
            };

             _mockOfApi = new Mock<ITFLAPIClient>();

        }

        public void Dispose()
        {
            _sw.Dispose();
        }

        [Fact]
        public void CheckCallIsMadeToClientApi()
        {
            // Arrange
           

            _mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(_mockDataOfApi);
            var consoleApp = new ConsoleApp(_mockOfApi.Object);
           
            // Act
            consoleApp.ShowStatutOfTubeLines();

            // Assert
            _mockOfApi.Verify(m => m.SetupAndMakeApiCallAndReturnFormattedData(), Times.Once());
        }

        
        [Fact]
        public void CheckCallIsMadeToClientApiForVictoriaLine()
        {
            _mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(_mockDataOfApi);
            var consoleApp = new ConsoleApp(_mockOfApi.Object);

            //Act
            consoleApp.ShowStatutOfTubeLines();

            //Assert
            _mockOfApi.Verify(m => m.SetupAndMakeApiCallAndReturnFormattedData(), Times.Once());
        }
        

        [Fact]
        public void DisplayTheStatusOfAllTubeLines()
        {

                // Arrange        
                _mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(_mockDataOfApi);
                var consoleApp = new ConsoleApp(_mockOfApi.Object);    
                
                // Act
                consoleApp.ShowStatutOfTubeLines();
                string expected = string.Format("Victoria ------ Good Service  {0}Bakerloo ------ Good Service  {0}Circle ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, _sw.ToString());
                
            

        }

        [Fact]
        public void DisplayTheStatusOfVictoriaLine()
        {
          
                // Arrange       
                _mockOfApi.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(_mockDataOfApi);
                var consoleApp = new ConsoleApp(_mockOfApi.Object);

                // Act
                consoleApp.ShowStatutOfTubeLines("Victoria");
                string expected = string.Format("Victoria ------ Good Service  {0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, _sw.ToString());
   
        }

       

    }

}

