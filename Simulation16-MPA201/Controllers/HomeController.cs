using Microsoft.AspNetCore.Mvc;

namespace Simulation16_MPA201.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
