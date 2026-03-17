using Microsoft.AspNetCore.Mvc;

namespace SistemaGestorAutomoviles.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
