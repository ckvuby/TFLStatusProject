﻿using System.Diagnostics;
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

        public IActionResult Index()
        {
            var lineInformationData = tflApiClient.SetupAndMakeApiCallAndReturnFormattedData();
            //HttpContext.Response.Headers.Add("refresh", "300; url=" + Url.Action("Index"));
            return View(lineInformationData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
