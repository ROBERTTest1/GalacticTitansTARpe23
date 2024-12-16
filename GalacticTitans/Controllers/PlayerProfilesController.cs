using GalacticTitans.Data;
using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class PlayerProfilesController : Controller
    {
        private readonly GalacticTitansContext _context;
        public PlayerProfilesController(GalacticTitansContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View( _context.PlayerProfiles.OrderByDescending(x => x.ScreenName));
        }
        //[HttpGet]
        //public async Task<Player>

        //[HttpGet]
        // method that gets the user the view for playerprofile info

        //[HttpPost]
        // method to generate new playerprofile, info is gotten from a view
        // that the player is directed to, right after confirmation.

        //[HttpGet]
        // method FOR ADMINS to get view for player profile modification

        //[HttpGet]
        // method FOR USERS to get SETTINGS view for player profile modification.

        //[HttpPost]
        // method FOR ADMINS and USERS to modify player profile
    }
}
