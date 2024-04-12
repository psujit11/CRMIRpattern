using Microsoft.AspNetCore.Mvc;

namespace CRMWeb.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
