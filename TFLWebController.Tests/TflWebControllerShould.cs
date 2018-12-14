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
        public void TReturn_IndexView_With_TubeLines_ListWithServiceStatus()
        {
            Mock<ITFLAPIClient> mockITFLApiClient = new Mock<ITFLAPIClient>();
            HomeController sut = new HomeController(mockITFLApiClient.Object);
            IActionResult result = sut.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
