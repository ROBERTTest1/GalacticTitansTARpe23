using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Models;
using GalacticTitans.Models.AstralBodies;
using GalacticTitans.Models.Titans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace GalacticTitans.Controllers
{
    public class AstralBodiesController : Controller
    {
        private readonly GalacticTitansContext _context;
        private readonly IFileServices _fileServices;
        private readonly IAstralBodiesServices _astralBodiesServices;
        private readonly ISolarSystemsServices _solarSystemsServices;
        public AstralBodiesController
            (
            GalacticTitansContext context, 
            IAstralBodiesServices astralBodiesServices, 
            IFileServices fileServices,
            ISolarSystemsServices solarSystemsServices
            )
        {
            _context = context;
            _fileServices = fileServices;
            _astralBodiesServices = astralBodiesServices;
            _solarSystemsServices = solarSystemsServices;
        }
        /// <summary>
        /// Index for admin view of planets
        /// </summary>
        /// <returns></returns>
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
                    SolarSystemID = (Guid)x.SolarSystemID,
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

        /// <summary>
        /// Create get for admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            AstralBodyCreateUpdateViewModel vm = new();
            return View("CreateUpdate", vm);
        }

        /// <summary>
        /// create post for admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AstralBodyCreateUpdateViewModel vm)
        {
            // this make new, do not add guid
            var dto = new AstralBodyDto()
            {
                AstralBodyName = vm.AstralBodyName,
                AstralBodyType = vm.AstralBodyType,
                EnvironmentBoost = (Core.Dto.TitanType)vm.EnvironmentBoost,
                AstralBodyDescription = vm.AstralBodyDescription,
                MajorSettlements = vm.MajorSettlements,
                TechnicalLevel = vm.TechnicalLevel,
                //no titan owns a planet that has been created by admin
                //and no planet is assigned to a solar system in the planet creation view
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    AstralBodyID = x.AstralBodyID
                }).ToArray()
            };
            var addedPlanet = await _astralBodiesServices.Create(dto);
            if (addedPlanet == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", vm);
        }

        /// <summary>
        /// details for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Details(Guid id)
        {
            ViewData["RequestedView"] = "Details";
            var dto = await _astralBodiesServices.DetailsAsync(id);

            if (dto == null)
            {
                List<string> errordatas = ["Area", "Planets", "Issue", "var dto == null", "StatusMessage", "This planet was not found"];
                ViewBag.ErrorDatas = errordatas;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            var images = await _context.FilesToDatabase
                .Where(x => x.AstralBodyID == id)
                .Select(y => new AstralBodyImageViewModel
                {
                    AstralBodyID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new AstralBodyDetailsDeleteViewModel();

            vm.ID = dto.ID;
            vm.AstralBodyName = dto.AstralBodyName;
            vm.AstralBodyDescription = dto.AstralBodyDescription;
            vm.TechnicalLevel = dto.TechnicalLevel;
            vm.MajorSettlements = dto.MajorSettlements;
            vm.AstralBodyType = dto.AstralBodyType;
            vm.EnvironmentBoost = dto.EnvironmentBoost;
            vm.SolarSystemID = dto.SolarSystemID;
            vm.CreatedAt = dto.CreatedAt;
            vm.ModifiedAt = dto.ModifiedAt;
            vm.Image.AddRange(images);

            return View("DetailsDelete", vm);
        }

        /// <summary>
        /// update get for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Update(Guid id)
        {
            var astralBodyToBeUpdated = await _astralBodiesServices.DetailsAsync(id);
            if (astralBodyToBeUpdated == null)
            {
                List<string> errordatas = ["Area", "Planets", "Issue", "var astralBodyToBeUpdated == null", "StatusMessage", "Planet with this ID is null"];
                ViewBag.ErrorDatas = errordatas;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            var images = await _context.FilesToDatabase
                .Where(x => x.AstralBodyID == id)
                .Select(y => new AstralBodyImageViewModel
                {
                    AstralBodyID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new AstralBodyCreateUpdateViewModel();

            vm.ID = astralBodyToBeUpdated.ID;
            vm.AstralBodyName = astralBodyToBeUpdated.AstralBodyName;
            vm.AstralBodyType = astralBodyToBeUpdated.AstralBodyType;
            vm.EnvironmentBoost = (Models.Titans.TitanType)astralBodyToBeUpdated.EnvironmentBoost;
            vm.AstralBodyDescription = astralBodyToBeUpdated.AstralBodyDescription;
            vm.MajorSettlements = astralBodyToBeUpdated.MajorSettlements;
            vm.TechnicalLevel = astralBodyToBeUpdated.TechnicalLevel;
            vm.TitanWhoOwnsThisPlanet = astralBodyToBeUpdated.TitanWhoOwnsThisPlanet;
            vm.SolarSystemID = astralBodyToBeUpdated.SolarSystemID;
            vm.CreatedAt = astralBodyToBeUpdated.CreatedAt;
            vm.UpdatedAt = astralBodyToBeUpdated.ModifiedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        /// <summary>
        /// update post for admin
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        //for the time being, create and update post methods are separate with only 2
        //differences between them, in the future, these can be combined into one method
        //it is 23:01 and i cant be assed to figure that shit our rn, so this is what you get.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(AstralBodyCreateUpdateViewModel vm)
        {
            // this make new, do not add guid
            var dto = new AstralBodyDto()
            {
                ID = (Guid)vm.ID,
                AstralBodyName = vm.AstralBodyName,
                AstralBodyType = vm.AstralBodyType,
                EnvironmentBoost = (Core.Dto.TitanType)vm.EnvironmentBoost,
                AstralBodyDescription = vm.AstralBodyDescription,
                MajorSettlements = vm.MajorSettlements,
                TechnicalLevel = vm.TechnicalLevel,
                //no titan owns a planet that has been created by admin
                //and no planet is assigned to a solar system in the planet creation view
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    AstralBodyID = x.AstralBodyID
                }).ToArray()
            };
            var addedPlanet = await _astralBodiesServices.Update(dto);
            if (addedPlanet == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", vm);
        }

        /// <summary>
        /// delete get for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Delete(Guid id)
        {
            ViewData["RequestedView"] = "Delete";
            var deletableAstralBody = await _astralBodiesServices.DetailsAsync(id);
            if (deletableAstralBody == null)
            {
                List<string> errordatas = ["Area", "Planets", "Issue", "var deletableAstralBody == null", "StatusMessage", "This planet was not found"];
                ViewBag.ErrorDatas = errordatas;
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            var images = await _context.FilesToDatabase
                .Where(x => x.AstralBodyID == id)
                .Select(y => new AstralBodyImageViewModel
                {
                    AstralBodyID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new AstralBodyDetailsDeleteViewModel();

            vm.ID = deletableAstralBody.ID;
            vm.AstralBodyName = deletableAstralBody.AstralBodyName;
            vm.AstralBodyDescription = deletableAstralBody.AstralBodyDescription;
            vm.TechnicalLevel = deletableAstralBody.TechnicalLevel;
            vm.MajorSettlements = deletableAstralBody.MajorSettlements;
            vm.AstralBodyType = deletableAstralBody.AstralBodyType;
            vm.EnvironmentBoost = deletableAstralBody.EnvironmentBoost;
            vm.SolarSystemID = deletableAstralBody.SolarSystemID;
            vm.CreatedAt = deletableAstralBody.CreatedAt;
            vm.ModifiedAt = DateTime.Now;
            vm.Image.AddRange(images);

            return View("DetailsDelete", vm);
        }

        /// <summary>
        /// delete post for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var astralBodyId = await _astralBodiesServices.Delete(id);
            if (astralBodyId == null) 
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult SolarSystemExplore(Guid id)
        {
            var thisSystemPlanets = _context.AstralBodies
                .OrderByDescending(y => y.CreatedAt)
                .Where(z => z.SolarSystemID == id)
                .Select(x => new AstralBodyIndexViewModel
                {
                    ID = x.ID,
                    AstralBodyName = x.AstralBodyName,
                    AstralBodyType = x.AstralBodyType,
                    EnvironmentBoost = (Models.Titans.TitanType)x.EnvironmentBoost,
                    MajorSettlements = x.MajorSettlements,
                    TechnicalLevel = x.TechnicalLevel,
                    SolarSystemID = (Guid)x.SolarSystemID,
                    Image = (List<AstralBodyImageViewModel>)_context.FilesToDatabase
                       .Where(t => t.AstralBodyID == x.ID)
                       .Select(z => new AstralBodyImageViewModel
                       {
                           AstralBodyID = z.ID,
                           ImageID = z.ID,
                           ImageData = z.ImageData,
                           ImageTitle = z.ImageTitle,
                           Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                       })
                });
            var thisSystem = _context.SolarSystems
                .Where(z => z.ID == id)
                .Select(x => new SolarSystemExploreViewModel
                {
                    ID = x.ID,
                    AstralBodyAtCenter = x.AstralBodyAtCenter,
                    SolarSystemName = x.SolarSystemName,
                    SolarSystemLore = x.SolarSystemLore,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    Planets = (List<AstralBodyIndexViewModel>)thisSystemPlanets
                });

            return View(thisSystem);
        }


        [HttpGet]
        public IActionResult SolarSystemAdminIndex()
        {
            var allSystems = _context.SolarSystems
                .OrderByDescending(y => y.SolarSystemName)
                .Select(x => new SolarSystemAdminIndexViewModel
                {
                    ID = x.ID,
                    AstralBodyAtCenter = x.AstralBodyAtCenter,
                    SolarSystemName = x.SolarSystemName,
                    SolarSystemLore = x.SolarSystemLore,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                });
            return View(allSystems);
        }

        [HttpGet]
        public IActionResult SolarSystemCreate()
        {
            SolarSystemCreateUpdateViewModel vm = new();
            return View("SolarSystemCreateUpdate", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolarSystemCreate(SolarSystemCreateUpdateViewModel vm, List<AstralBody> planets)
        {
            // this make new, do not add guid
            var dto = new SolarSystemDto()
            {
                SolarSystemName = vm.SolarSystemName,
                SolarSystemLore = vm.SolarSystemLore,
                AstralBodyAtCenter = vm.AstralBodyAtCenter,
                AstralBodyIDs = vm.AstralBodyIDs,
                Planets = planets,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };


            var newSystem = await _solarSystemsServices.Create(dto);
            if (newSystem == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", vm);
        }

        [HttpGet]
        public IActionResult GalacticConquest()
        {
            var allSolarSystems = _context.SolarSystems
                .OrderBy(y => y.UpdatedAt)
                .Select(x => new SolarSystemExploreViewModel
                {
                    ID = x.ID,
                    AstralBodyAtCenter = x.AstralBodyAtCenter,
                    SolarSystemName = x.SolarSystemName,
                    SolarSystemLore = x.SolarSystemLore,
                    CreatedAt= x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).Take(16);
            return View(allSolarSystems);
        }
    }
}
