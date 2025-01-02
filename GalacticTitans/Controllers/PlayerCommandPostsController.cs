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
        public IActionResult Index2()
        {
            ViewData["StoryValue"] = 1;
            //return View("~./Views/Testing/PlayerCommandPost.cshtml");
            return View();
        }
    }
}
