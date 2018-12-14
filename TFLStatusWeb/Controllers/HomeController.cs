using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        public IActionResult Index()
        {
            var lineInformationData = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            //HttpContext.Response.Headers.Add("refresh", "300; url=" + Url.Action("Index"));
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
