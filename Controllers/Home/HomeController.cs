using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Smart_Games.DAO;
using Smart_Games.Models;
using System.Diagnostics;

namespace Smart_Games.Controllers.Home
{
    public class HomeController : Controller
    {

        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            ViewBag.listAllGames = DAO.DAO.listAllGames(_configuration);

            return View();
        }

    }
}
