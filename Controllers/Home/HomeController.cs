using Microsoft.AspNetCore.Mvc;
using Smart_Games.Models;
using System.Diagnostics;

namespace Smart_Games.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
