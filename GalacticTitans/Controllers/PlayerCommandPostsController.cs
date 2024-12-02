using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class PlayerCommandPostsController : Controller
    {
        public IActionResult Index()
        {
            //return View("~./Views/Testing/PlayerCommandPost.cshtml");
            return View();
        }
    }
}
