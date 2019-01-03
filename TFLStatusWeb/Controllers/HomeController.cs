using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TFLStatusLibrary;
using TFLStatusWeb.Models;


namespace TFLStatusWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITFLAPIClient tflApiClient;

        public HomeController(ITFLAPIClient tflapiClient)
        {
            
            tflApiClient = tflapiClient;
           

        }

        public ViewResult Index()
        {

            var lineInformationData = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            return View(lineInformationData);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
