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

        public IActionResult Customers()
        {
            ViewBag.Message = "Customers Information Page";
            ViewBag.FullPageIntro = false;
            ViewBag.RenderCarousel = false;
            return View();
        }

        public IActionResult Leads()
        {
            ViewBag.Message = "Lead Management Page";
            ViewBag.FullPageIntro = false;
            ViewBag.RenderCarousel = false;
            return View();
        }

        public IActionResult Opportunities()
        {
            ViewBag.Message = "Opportunities Management Page";
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
