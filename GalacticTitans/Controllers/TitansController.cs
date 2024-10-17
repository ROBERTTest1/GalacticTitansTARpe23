using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class TitansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
