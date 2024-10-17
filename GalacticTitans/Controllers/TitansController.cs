using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Models.Titans;
using Microsoft.AspNetCore.Mvc;

namespace GalacticTitans.Controllers
{
    public class TitansController : Controller
    {
        /*
         * Titanscontroller controls all functions for titans, including, missions.
         */

        private readonly GalacticTitansContext _context;
        private readonly ITitansServices _titansServices;

        public TitansController(GalacticTitansContext context, ITitansServices titansServices)
        {
            _context = context;
            _titansServices = titansServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var resultingInventory = _context.Titans
                .OrderByDescending(y => y.TitanLevel)
                .Select(x => new TitanIndexViewModel
                {
                    ID = x.ID,
                    TitanName = x.TitanName,
                    TitanType = (TitanType)x.TitanType,
                    TitanLevel = x.TitanLevel,                    
                });
            return View(resultingInventory);
        }
    }
}
