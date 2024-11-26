using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class AdminAreasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
