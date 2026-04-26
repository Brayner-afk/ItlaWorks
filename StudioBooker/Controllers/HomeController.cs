using Microsoft.AspNetCore.Mvc;

namespace StudioBooker.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
