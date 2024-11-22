using Microsoft.AspNetCore.Mvc;
using MVC1.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Calc(HeatCalculationModel model)
        {
            
            if (ModelState.IsValid)
            {
                var results = model.CalculateTable();
                ViewBag.Results = results;

                ViewBag.ChartDataY = results.Select(r => r._y).ToList();
                ViewBag.ChartDataTemperatureMaterial = results.Select(r => r.TemperatureMaterial).ToList();
                ViewBag.ChartDataTemperatureGas = results.Select(r => r.TemperatureGas).ToList();
                ViewBag.ChartDataTemperatureDifference = results.Select(r => r.TemperatureDifference).ToList();

                TempData["Results"] = JsonConvert.SerializeObject(results);

                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ExportToExcel(HeatCalculationModel model)
        {
            if (TempData["Results"] == null)
            {
                return RedirectToAction("Index");
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var results = JsonConvert.DeserializeObject<List<CalculationResult>>(TempData["Results"].ToString());

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                                
                worksheet.Cells[1, 1].Value = "Координата y, м";
                worksheet.Cells[1, 2].Value = "Температура материала, °C";
                worksheet.Cells[1, 3].Value = "Температура газа, °C";
                worksheet.Cells[1, 4].Value = "Разность температур, °C";

                int row = 2;
                foreach (var result in results)
                {
                    worksheet.Cells[row, 1].Value = result._y;
                    worksheet.Cells[row, 2].Value = result.TemperatureMaterial;
                    worksheet.Cells[row, 3].Value = result.TemperatureGas;
                    worksheet.Cells[row, 4].Value = result.TemperatureDifference;
                    row++;
                }

                worksheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = "Report.xlsx";
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(stream, contentType, fileName);
            }
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
