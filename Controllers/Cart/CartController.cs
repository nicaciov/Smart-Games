using Microsoft.AspNetCore.Mvc;

namespace Smart_Games.Controllers.Cart
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
