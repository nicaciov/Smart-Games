using Microsoft.AspNetCore.Mvc;
using Smart_Games.DAO;
using Smart_Games.Models;
using System.Configuration;

namespace Smart_Games.Controllers.Cart
{
    public class CartController : Controller
    {

        private readonly IConfiguration _configuration;

        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Cart()
        {

            List<CartModel> list = DAO.DAO.listCart(_configuration);
            List<GameModel> listCart = new List<GameModel>();

            for (int i = 0; i < list.Count; i++)
            {
                listCart.Add(DAO.DAO.getGame(_configuration, list[i].game_id));
            }

            ViewBag.listCart = listCart;

            List<GameModel> cartItems = listCart;

            // Agrupa os itens pelo ID do jogo e calcula as estatísticas
            var groupedItems = cartItems
                .GroupBy(item => item.id)
                .Select(group => new
                {
                    GameId = group.Key,
                    GameName = group.First().game_name,
                    Quantity = group.Count(),
                    Subtotal = group.Sum(item => item.game_price)
                })
                .ToList();

            // Calcula o total geral
            double total = groupedItems.Sum(item => item.Subtotal);

            ViewBag.GroupedCartItems = groupedItems;
            ViewBag.Total = total;

            return View();


        }

        public IActionResult AddCart(int id)
        {

            DAO.DAO.addCart(_configuration, id); 

            return RedirectToAction("Cart", "Cart");

        }

        public IActionResult RemoveCart(int id)
        {

            DAO.DAO.removeCart(_configuration, id);

            return RedirectToAction("Cart", "Cart");

        }

        public IActionResult ClearCart()
        {

            DAO.DAO.clearCart(_configuration);

            return RedirectToAction("Cart", "Cart");

        }

    }
}
