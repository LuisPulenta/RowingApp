using RowingApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace RowingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()

        {
            // Obtener la versión de .NET
            var dotnetVersion = System.Environment.Version.ToString();

            // Pasar la versión a la vista
            ViewBag.DotnetVersion = dotnetVersion;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Esta es una App Genérica.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Por consultas comunicarse con:";

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

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}
