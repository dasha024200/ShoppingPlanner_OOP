using Microsoft.AspNetCore.Mvc;

namespace ShoppingPlanner_OOP.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}