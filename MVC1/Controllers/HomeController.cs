using Microsoft.AspNetCore.Mvc;
using MVC1.Models;
using System.Diagnostics;

namespace MVC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Calc(double H0, double T_m, double T_g, double w_g, double C_g, double G_m, double C_m, double alpha_V, double D)
        {
            //var result = operationType switch
            //{
            //    1 => num1 + num2,
            //    2 => num1 - num2,
            //    3 => num1 * num2,
            //    4 => num1 / num2,
            //};

            //ViewData["result"] = result;

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
