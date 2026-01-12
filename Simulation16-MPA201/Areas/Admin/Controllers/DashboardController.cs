using Microsoft.AspNetCore.Mvc;

namespace Simulation16_MPA201.Areas.Admin.Controllers;
[Area("Admin")]

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
