using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EP.DemoLicoes.UI.Models;
using Microsoft.Extensions.Configuration;

namespace EP.DemoLicoes.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.Frase = _configuration.GetValue<string>("Frase");
            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception("THE BOMB HAS BEEN PLANTED!!!!");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string error)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
