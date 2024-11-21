using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Models.AstralBodies;
using GalacticTitans.Models.Titans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalacticTitans.Controllers
{
    public class AstralBodiesController : Controller
    {
        private readonly GalacticTitansContext _context;
        private readonly IFileServices _fileServices;
        private readonly IAstralBodiesServices _astralBodiesServices;
        public AstralBodiesController(GalacticTitansContext context, IAstralBodiesServices astralBodiesServices, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
            _astralBodiesServices = astralBodiesServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allPlanets = _context.AstralBodies
                .OrderByDescending(y => y.AstralBodyType)
                .Select(x => new AstralBodyIndexViewModel
                {
                    ID = x.ID,
                    AstralBodyName = x.AstralBodyName,
                    AstralBodyType = x.AstralBodyType,
                    EnvironmentBoost = (Models.Titans.TitanType)x.EnvironmentBoost,
                    MajorSettlements = x.MajorSettlements,
                    TechnicalLevel = x.TechnicalLevel,
                    SolarSystemID = x.SolarSystemID,
                    //Image = (List<AstralBodyIndexViewModel>)_context.FilesToDatabase
                    //   .Where(t => t.TitanID == x.ID)
                    //   .Select(z => new AstralBodyIndexViewModel
                    //   {
                    //       TitanID = z.ID,
                    //       ImageID = z.ID,
                    //       ImageData = z.ImageData,
                    //       ImageTitle = z.ImageTitle,
                    //       Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                    //   })
                });
            return View(allPlanets);
        }
    }
}
