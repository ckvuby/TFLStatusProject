using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TFLStatusLibrary;
using TFLStatusWeb.Models;


namespace TFLStatusWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITFLAPIClient _tflapiClient;

        public HomeController(ITFLAPIClient tflapiClient)
        {   
            _tflapiClient = tflapiClient;
        }

        public ViewResult Index()
        {

            var lineInformationData = _tflapiClient.SetupAndMakeApiCallAndReturnFormattedData();
            return View(lineInformationData);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
