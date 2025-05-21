using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Primarie_Craiova.Models;

namespace Primarie_Craiova.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

 
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Citizens()
    {
        return View();
    }

    [Authorize]
    public IActionResult Complaints()
    {
        return View();
    }

    [Authorize]
    public IActionResult Employees()
    {
        return View();
    }

    [Authorize]
    public IActionResult Departments()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
