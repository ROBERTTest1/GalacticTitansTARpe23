using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class PlayerProfilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
