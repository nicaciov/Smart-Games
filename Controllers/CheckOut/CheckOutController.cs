using Microsoft.AspNetCore.Mvc;

namespace Smart_Games.Controllers.CheckOut
{
    public class CheckOutController : Controller
    {
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
