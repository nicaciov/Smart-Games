using Microsoft.AspNetCore.Mvc;
using Smart_Games.Models;

namespace Smart_Games.Controllers.Game
{
    public class GameController : Controller
    {
        private readonly IConfiguration _configuration;

        public GameController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult ViewGame(int id)
        {

            GameModel gameModel = DAO.DAO.getGame(_configuration, id);

            if (gameModel != null)
            {

                ViewBag.listAllGames = DAO.DAO.listAllGamesRand(_configuration);
                return View(gameModel);

            }
            else
            {

                return RedirectToAction("Home", "Index");

            }

        }

        public IActionResult NextStore(int id, string lat, string lon)
        {

            GameModel gameModel = DAO.DAO.getGame(_configuration, id);

            if (gameModel != null)
            {

                ViewBag.listAllGames = DAO.DAO.listAllGamesRand(_configuration);

                StoreModel nextStore = TOOLBOX.CoordinateDistanceCalculator.FindClosestCoordinate(id, lat, lon, _configuration);
                ViewBag.nextStore = nextStore;

                return View(gameModel);

            }
            else
            {

                return RedirectToAction("Home", "Index");

            }

        }

    }
}
