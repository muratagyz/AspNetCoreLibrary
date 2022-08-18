using ErrorHandling.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ErrorHandling.Filter;
using Microsoft.AspNetCore.Diagnostics;

namespace ErrorHandling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [CustomHandleExceptionFilter(ErrorPage = "Error2")]
        public IActionResult Index()
        {
            //throw new Exception("Veri tabanı bağlanamadı");
            int value1 = 5;
            int value2 = 0;

            int result = value1 / value2;

            return View();
        }

        public IActionResult Privacy()
        {
            //throw new FileNotFoundException();
            return View();
        }

        public IActionResult Error1()
        {
            return View();
        }

        public IActionResult Error2()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.path = exception.Path;
            ViewBag.message = exception.Error.Message;

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}