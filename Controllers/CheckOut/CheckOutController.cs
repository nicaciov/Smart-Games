using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using Smart_Games.Models;
using System.Diagnostics.Metrics;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;

namespace Smart_Games.Controllers.CheckOut
{
    public class CheckOutController : Controller
    {
        readonly IConfiguration _configuration;

        public CheckOutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult CheckOut()
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

        public IActionResult ApplyPromotionalCode(string code)
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

            // Verifica se o código do cupom é "DEZCONTO" e aplica o desconto de 10%
            if (code != null && code.ToUpper() == "DEZCONTO")
            {
                double desconto = total * 0.10; // 10% de desconto
                total -= desconto;
                ViewBag.desconto = desconto;

            }

            ViewBag.GroupedCartItems = groupedItems;
            ViewBag.Total = total;

            return View();
        }

        public IActionResult FinishCheckOut(string name, string family_name, string email, string cpf, string address,
            string address_complement, string country, string state, string cep, string type_payment_credit,
            string type_payment_debit, string type_payment_paypal, string card_name, string card_number,
            string card_date_expiration, string card_cvv, string code)
        {
            CheckOutModel checkOut = new CheckOutModel();

            checkOut.name = name;
            checkOut.family_name = family_name;
            checkOut.email = email;
            checkOut.cpf = cpf;
            checkOut.address = address;
            checkOut.address_complement = address_complement;
            checkOut.country = country;
            checkOut.state = state;
            checkOut.cep = cep;
            checkOut.type_payment_credit = type_payment_credit;
            checkOut.type_payment_debit = type_payment_debit;
            checkOut.type_payment_paypal = type_payment_paypal;
            checkOut.card_name = card_name;
            checkOut.card_number = card_number;
            checkOut.card_date_expiration = card_date_expiration;
            checkOut.card_cvv = card_cvv;
            checkOut.code = code;


            long idCheckOut = DAO.DAO.addCheckout(_configuration, checkOut);

            List<CartModel> list = DAO.DAO.listCart(_configuration);
            List<GameModel> listCart = new List<GameModel>();


            for (int i = 0; i < list.Count; i++)
            {
                DAO.DAO.addCheckout_Shop_Itens(_configuration, list[i].game_id, idCheckOut);
            }
            
            DAO.DAO.clearCart(_configuration);

            list = null;
            listCart = null;

             list = DAO.DAO.listCheckOutItens(_configuration, idCheckOut);
             listCart = new List<GameModel>();

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



}
}
