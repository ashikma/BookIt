using Microsoft.AspNetCore.Mvc;

namespace BookItWebApplication.Controllers
{
    public class BarController : Controller
    {
        public IActionResult StartPage()
        {
            return View();
        }
    }
}
