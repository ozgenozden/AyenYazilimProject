using ApiFunction;
using AyenProjectApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AyenProjectApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AyenApi _ayenApi;

        public HomeController(ILogger<HomeController> logger,AyenApi ayenApi)
        {
            _logger = logger;
            _ayenApi = ayenApi;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var res = _ayenApi.GetAyenProduct();
            return Json(new { data = res });
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
