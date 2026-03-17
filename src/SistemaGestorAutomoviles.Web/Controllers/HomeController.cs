using Microsoft.AspNetCore.Mvc;

namespace SistemaGestorAutomoviles.Web.Controllers;

public class HomeController(IConfiguration configuration) : Controller
{
    public IActionResult Index()
    {
        ViewBag.ApiBaseUrl = configuration["ApiSettings:BaseUrl"];
        return View();
    }
}
