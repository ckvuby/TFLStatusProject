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

                Console.SetOut(sw);

                // Act
                consoleApp.ShowStatusOfAllTubeLines(options);
                string expected = string.Format("Victoria ------ Good Service{0}Bakerloo ------ Good Service{0}Circle ------ Good Service{0}", Environment.NewLine);

                // Assert
                Assert.Equal(expected, sw.ToString());
            }

        }
    }
    
}

