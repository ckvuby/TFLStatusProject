using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
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
          
            Mock<IHttpClient> httpClient = new Mock<IHttpClient>();
            var url = new Uri("https://api.tfl.gov.uk/line/mode/tube/status?detail=true");


            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
              
                Content = new StringContent("[{\"$type\":\"Tfl.Api.Presentation.Entities.Line, Tfl.Api.Presentation.Entities\",\"id\":\"bakerloo\",\"name\":\"Bakerloo\",\"modeName\":\"tube\",\"disruptions\":[],\"created\":\"2018-11-28T11:37:03.687Z\",\"modified\":\"2018-11-28T11:37:03.687Z\",\"lineStatuses\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineStatus, Tfl.Api.Presentation.Entities\",\"id\":0,\"statusSeverity\":10,\"statusSeverityDescription\":\"Good Service\",\"created\":\"0001-01-01T00:00:00\",\"validityPeriods\":[]}],\"routeSections\":[],\"serviceTypes\":[{\"$type\":\"Tfl.Api.Presentation.Entities.LineServiceTypeInfo, Tfl.Api.Presentation.Entities\",\"name\":\"Regular\",\"uri\":\"/Line/Route?ids=Bakerloo&serviceTypes=Regular\"}],\"crowding\":{\"$type\":\"Tfl.Api.Presentation.Entities.Crowding, Tfl.Api.Presentation.Entities\"}}]")
            };

            httpClient.Setup(x => x.GetAsync(url)).Returns(Task.FromResult<HttpResponseMessage>(responseMessage));        
            TFLApiClient tflClient = new TFLApiClient(httpClient.Object, url);

            var response = tflClient.SetupAndMakeApiCallAndReturnFormattedData().ToList();

            HomeController homeController = new HomeController(tflClient);
            IActionResult result = homeController.Index();


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

            var viewResult = Assert.IsType<ViewResult>(result);

            Assert.NotNull(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LineInformation>>(viewResult.ViewData.Model);
            Assert.Equal(expectedLineInformation[0].lineId, model.ElementAt(0).lineId);

        
        }
    }
}
