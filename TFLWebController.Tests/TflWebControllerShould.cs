using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
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

            IEnumerable<LineInformation> expectedLineInformation = new List<LineInformation>
                {
                    new LineInformation
                    {
                        LineId = "bakerloo",
                        LineName = "Bakerloo",
                        LineStatus = "Good Service",
                        StatusReason = null
                    }
                };

            tflApiClient.Setup(x => x.SetupAndMakeApiCallAndReturnFormattedData()).ReturnsAsync(expectedLineInformation);
            HomeController homeController = new HomeController(tflApiClient.Object);

            //Act             
            ViewResult result = homeController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result.ViewData);
            var model = Assert.IsAssignableFrom<IEnumerable<LineInformation>>(result.ViewData.Model);
            Assert.Equal(expectedLineInformation.ElementAt(0), model.FirstOrDefault());


        }
    }
}
