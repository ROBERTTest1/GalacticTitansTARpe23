using GalacticTitans.Core.Dto;
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
                    TitanType = (Models.Titans.TitanType)(Core.Dto.TitanType)x.TitanType,
                    TitanLevel = x.TitanLevel,                    
                });
            return View(resultingInventory);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            TitanCreateViewModel vm = new();
            return View("Create",vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(TitanCreateViewModel vm)
        {
            var dto = new TitanDto()
            {
                TitanName = vm.TitanName,
                TitanHealth = 100,
                TitanXP = 0,
                TitanXPNextLevel = 100,
                TitanLevel = 0,
                TitanType = (Core.Dto.TitanType)vm.TitanType,
                TitanStatus = (Core.Dto.TitanStatus)vm.TitanStatus,
                PrimaryAttackName = vm.PrimaryAttackName,
                PrimaryAttackPower = vm.PrimaryAttackPower,
                SecondaryAttackName = vm.SecondaryAttackName,
                SecondaryAttackPower = vm.SecondaryAttackPower,
                SpecialAttackName = vm.SpecialAttackName,
                SpecialAttackPower = vm.SpecialAttackPower,
                TitanWasBorn = vm.TitanWasBorn,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Files = vm.Files,
                Image = vm.Image
                .Select(x => new FileToDatabaseDto
                {
                    ID = x.ImageID,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    TitanID = x.TitanID,
                }).ToArray()
            };
            var result = await _titansServices.Create(dto);

            if (result != null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", vm);
        }
    }
}
