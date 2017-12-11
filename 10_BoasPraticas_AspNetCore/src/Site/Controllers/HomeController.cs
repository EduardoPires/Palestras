using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration _configuration { get; set; }

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Key = _configuration["Key"];
            ViewBag.Password = _configuration["Password"];

            ViewBag.Cultura = CultureInfo.CurrentCulture.DisplayName;

            return View();
        }
    }
}
