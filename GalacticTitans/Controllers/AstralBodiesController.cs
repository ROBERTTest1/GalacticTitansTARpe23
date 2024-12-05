using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Models;
using GalacticTitans.Models.AstralBodies;
using GalacticTitans.Models.Titans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .Where(z => z.SolarSystemID == id.ToString())
                .Select(x => new AstralBodyIndexViewModel
                {
                    ID = x.ID,
                    AstralBodyName = x.AstralBodyName,
                    AstralBodyType = x.AstralBodyType,
                    EnvironmentBoost = (Models.Titans.TitanType)x.EnvironmentBoost,
                    MajorSettlements = x.MajorSettlements,
                    TechnicalLevel = x.TechnicalLevel,
                    SolarSystemID = x.SolarSystemID,
                    Image = _context.FilesToDatabase
                       .Where(t => t.AstralBodyID == x.ID)
                       .Select(z => new AstralBodyImageViewModel
                       {
                           AstralBodyID = z.ID,
                           ImageID = z.ID,
                           ImageData = z.ImageData,
                           ImageTitle = z.ImageTitle,
                           Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                       }).ToList()
                }).ToList();
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
                    Planets = thisSystemPlanets
                }).ToList();

            //var result = thisSystem.ToList();

            return View("SolarSystemExplore", thisSystem.First());
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
        public async Task<IActionResult> SolarSystemCreate()
        {
            ViewData["userHasSelected"] = new List<string>();
            SolarSystemCreateUpdateViewModel vm = new();
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
            
            vm.Planets = allPlanets.ToList();
            ViewData["allPlanets"] = new SelectList(allPlanets, "ID","AstralBodyName", allPlanets);
            //ViewData["selectedPlanets"] = vm.AstralBodyIDs;
            return View("SolarSystemCreateUpdate", vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolarSystemCreate(SolarSystemCreateUpdateViewModel vm, List<string> userHasSelected, List<AstralBody> planets)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            ViewData["userHasSelected"] = tempParse; /*opcheck: ids correctly obtained*/

            // this make new, do not add guid
            var dto = new SolarSystemDto() { };
            dto.SolarSystemName = vm.SolarSystemName;
            dto.SolarSystemLore = vm.SolarSystemLore;
            dto.AstralBodyAtCenter = vm.AstralBodyAtCenter;
            dto.AstralBodyIDs = tempParse; /*opcheck: id correctly set in dbo*/
            dto.Planets = planets; /*opfail: no planets*/
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;
            if (dto.Planets == null || dto.Planets.Count() !> 0) /*debugfail: invert condition on second argument to LESSTHAN ONE*/
            {
                await Console.Out.WriteLineAsync("planets null");
                await Console.Out.WriteLineAsync("idcount" + dto.AstralBodyIDs.Count().ToString());
            }
            if (dto.AstralBodyIDs == null || dto.AstralBodyIDs.Count() !> 0) /*debugfail: bad condition*/
            {
                await Console.Out.WriteLineAsync("ids null"); /*debugfail: wrong error message here*/
                await Console.Out.WriteLineAsync("planetcount" + dto.Planets.Count().ToString());
            }
            //populate planets from ids
            if (dto.Planets !=  null && dto.AstralBodyIDs.Any())
            {
                dto.Planets = await IdToPlanet(dto.AstralBodyIDs); /*opcheck: correctly converts id to planet*/
            }
            //or populate ids from planets
            else if (!dto.AstralBodyIDs.Any() && dto.Planets.Any())
            {
                dto.AstralBodyIDs = await PlanetToID(dto.Planets);
            }
            planets = dto.Planets; //<--- added post next opcheck check
            //do nothing if there is (something or nothing) in both

            //make the system
            var newSystem = await _solarSystemsServices.Create(dto, planets); /*opcheck: planets is empty, should not be empty*/
            if (newSystem == null)
            {
                return RedirectToAction("SolarSystemAdminIndex");
            }
            return RedirectToAction("SolarSystemAdminIndex", vm);
        }
        [HttpGet]
        public async Task<IActionResult> SolarSystemUpdate(Guid id)
        {
            var modifyThisSystem = await _solarSystemsServices.DetailsAsync(id);
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
                    /*Image = (List<AstralBodyIndexViewModel>)_context.FilesToDatabase
                    //   .Where(t => t.TitanID == x.ID)
                    //   .Select(z => new AstralBodyIndexViewModel
                    //   {
                    //       TitanID = z.ID,
                    //       ImageID = z.ID,
                    //       ImageData = z.ImageData,
                    //       ImageTitle = z.ImageTitle,
                    //       Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                       })*/
                });
            //List<Guid> preselectedPreviously = PlanetToID(allPlanets.ToList());
            SolarSystemCreateUpdateViewModel vm = new();
            vm.ID = modifyThisSystem.ID;
            vm.SolarSystemName = modifyThisSystem.SolarSystemName;
            vm.SolarSystemLore = modifyThisSystem.SolarSystemLore;
            vm.AstralBodyAtCenter = modifyThisSystem.AstralBodyAtCenter;
            vm.AstralBodyIDs = modifyThisSystem.AstralBodyIDs;
            vm.AstralBodyAtCenterWith = modifyThisSystem.AstralBodyAtCenterWith;
            vm.CreatedAt = modifyThisSystem.CreatedAt;
            vm.UpdatedAt = modifyThisSystem.UpdatedAt;

            List<string> preselectedPreviously = new();
            List<AstralBodyIndexViewModel> planetSelection = new();

            foreach (var planet in allPlanets)
            {
                if (planet.SolarSystemID == modifyThisSystem.ID.ToString())
                {
                    preselectedPreviously.Add(planet.ID.ToString());
                    planetSelection.Add(planet);
                }
            }
            vm.Planets.AddRange(planetSelection);

            ViewData["userHasSelected"] = preselectedPreviously;
            ViewData["previouslySelected"] = preselectedPreviously;
            
            vm.Planets = allPlanets.ToList();
            ViewData["allPlanets"] = new SelectList(allPlanets, "ID", "AstralBodyName", allPlanets);
            //ViewData["selectedPlanets"] = vm.AstralBodyIDs;
            return View("SolarSystemCreateUpdate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SolarSystemUpdate(SolarSystemCreateUpdateViewModel vm, List<string> userHasSelected, List<string> previouslySelected, List<AstralBody> planets)
        {
            List<Guid> tempParse = new();
            List<Guid> tempParse2 = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            List<string> oldList = (List<string>)ViewData["previouslySelected"];
            previouslySelected = oldList;
            foreach (var stringID in previouslySelected)
            {
                tempParse2.Add(Guid.Parse(stringID));
            }

            var dto = new SolarSystemDto() { };
            dto.ID = vm.ID;
            dto.SolarSystemName = vm.SolarSystemName;
            dto.SolarSystemLore = vm.SolarSystemLore;
            dto.AstralBodyAtCenter = vm.AstralBodyAtCenter;
            dto.AstralBodyIDs = tempParse; 
            dto.Planets = planets;
            dto.CreatedAt = DateTime.Now;
            dto.UpdatedAt = DateTime.Now;

            //populate planets from ids
            if (dto.Planets != null && dto.AstralBodyIDs.Any())
            {
                dto.Planets = await IdToPlanet(dto.AstralBodyIDs); /*opcheck: correctly converts id to planet*/
            }
            //or populate ids from planets
            else if (!dto.AstralBodyIDs.Any() && dto.Planets.Any())
            {
                dto.AstralBodyIDs = await PlanetToID(dto.Planets);
            }
            planets = dto.Planets; //<--- added post next opcheck check
            //do nothing if there is (something or nothing) in both
            List<AstralBody> removedPlanets = await IdToPlanet(tempParse2);
            for (int i = 0; i < removedPlanets.Count(); i++)
            {
                if (planets.Contains(removedPlanets[i]))
                {
                    removedPlanets.Remove(removedPlanets[i]);
                }
            }

            //change the system
            var newSystem = await _solarSystemsServices.Update(dto, planets, removedPlanets);
            if (newSystem == null)
            {
                return RedirectToAction("SolarSystemAdminIndex");
            }
            return RedirectToAction("SolarSystemAdminIndex", vm);
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
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                }).Take(16);
            return View(allSolarSystems);
        }

        //private methods for use in controller only
        private async Task<List<Guid>> PlanetToID(List<AstralBody> planets)
        {
            var result = new List<Guid>();
            foreach (var planet in planets)
            {
                result.Add(planet.ID);
            }
            return result;
        }
        private List<Guid> PlanetToID(List<AstralBodyIndexViewModel> planets)
        {
            var result = new List<Guid>();
            foreach (var planet in planets)
            {
                result.Add(planet.ID);
            }
            return result;
        }

        private async Task<List<AstralBody>> IdToPlanet(List<Guid> astralBodyIDs)
        {
            var result = new List<AstralBody>();
            foreach (var id in astralBodyIDs)
            {
                result.Add( await _astralBodiesServices.DetailsAsync(id));
            }
            return result;
        }

        
    }
}
