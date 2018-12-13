using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TFLStatusLibrary;
using TFLStatusWeb.Models;
using TFLStatusWeb.Utility;

namespace TFLStatusWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private readonly ITFLAPIClient tflApiClient;

        public HomeController(IOptions<MySettingsModel> app, ITFLAPIClient tflapiClient)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
            tflApiClient = tflapiClient;
        }

        public IActionResult Index()
        {
            var lineInformationData = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            HttpContext.Response.Headers.Add("refresh", "300; url=" + Url.Action("Index"));
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
