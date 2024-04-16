using CRMWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CRMWeb.Controllers
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
            ViewBag.FullPageIntro = true;
            ViewBag.RenderCarousel = false;
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.FullPageIntro = false;
            ViewBag.RenderCarousel = false;
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.FullPageIntro = false;
            ViewBag.RenderCarousel = false;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
