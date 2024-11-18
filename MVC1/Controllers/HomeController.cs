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

        public IActionResult Calc(int num1, int num2, int operationType)
        {
            var result = operationType switch
            {
                1 => num1 + num2,
                2 => num1 - num2,
                3 => num1 * num2,
                4 => num1 / num2,
            };

            ViewData["result"] = result;
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
