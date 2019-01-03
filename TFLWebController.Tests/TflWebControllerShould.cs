using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TFLStatusLibrary;
using TFLStatusWeb.Controllers;
using Xunit;

namespace TFLWebController.Tests
{
    public class TflWebControllerShould
    {
        [Fact]
        public void Return_IndexView_With_TubeLinesListWithServiceStatus()
        {
            //Arrange
            var tflApiClient = new Mock<ITFLAPIClient>();
            var expectedLineInformation = new List<LineInformation>
                {
                    new LineInformation
                        {
                         lineId = "bakerloo",
                         lineName = "Bakerloo",
                         lineStatus = "Good Service",
                         statusReason = null
                        }
                };
            tflApiClient.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).Returns(expectedLineInformation);
            HomeController homeController = new HomeController(tflApiClient.Object);

            //Act
           IActionResult result = homeController.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
           
            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LineInformation>>(viewResult.ViewData.Model);       
            Assert.Equal(expectedLineInformation[0].lineId, model.ElementAt(0).lineId);
        }
    }
}
