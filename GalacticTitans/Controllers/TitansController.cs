using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using GalacticTitans.Models.Stories;
using GalacticTitans.Models.Titans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalacticTitans.Controllers
{
    public class TitansController : Controller
    {
        /*
         * Titanscontroller controls all functions for titans, including, missions.
         */

        private readonly GalacticTitansContext _context;
        private readonly ITitansServices _titansServices;
        private readonly IFileServices _fileServices;

        public TitansController(GalacticTitansContext context, ITitansServices titansServices, IFileServices fileServices)
        {
            _context = context;
            _titansServices = titansServices;
            _fileServices = fileServices;
        }


        /*
         
        T I T A N S 
         
         */
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
                    Image = (List<TitanImageViewModel>)_context.FilesToDatabase
                       .Where(t => t.TitanID == x.ID)
                       .Select(z => new TitanImageViewModel
                       {
                           TitanID = z.ID,
                           ImageID = z.ID,
                           ImageData = z.ImageData,
                           ImageTitle = z.ImageTitle,
                           Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(z.ImageData))
                       })
                });
            return View(resultingInventory);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            TitanCreateViewModel vm = new();
            return View("Create",vm);
        }
        [HttpPost , ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TitanCreateViewModel vm)
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

            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id /*, Guid ref*/)
        {
            var titan = await _titansServices.DetailsAsync(id);

            if (titan == null) 
            {
                return NotFound(); // <- TODO; custom partial view with message, titan is not located
            }

            var images = await _context.FilesToDatabase
                .Where(t => t.TitanID == id)
                .Select(y => new TitanImageViewModel
                {
                    TitanID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new TitanDetailsViewModel();
            vm.ID = titan.ID;
            vm.TitanName = titan.TitanName;
            vm.TitanHealth = titan.TitanHealth;
            vm.TitanXP = titan.TitanXP;
            vm.TitanLevel = titan.TitanLevel;
            vm.TitanType = (Models.Titans.TitanType)titan.TitanType;
            vm.TitanStatus = (Models.Titans.TitanStatus)titan.TitanStatus;
            vm.PrimaryAttackName = titan.PrimaryAttackName;
            vm.PrimaryAttackPower = titan.PrimaryAttackPower;
            vm.SecondaryAttackName = titan.SecondaryAttackName;
            vm.SecondaryAttackPower = titan.SecondaryAttackPower;
            vm.SpecialAttackName = titan.SpecialAttackName;
            vm.SpecialAttackPower = titan.SpecialAttackPower;
            vm.Image.AddRange(images);
            
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == null) { return NotFound(); }

            var titan = await _titansServices.DetailsAsync(id);

            if (titan == null) { return NotFound(); }

            var images = await _context.FilesToDatabase
                .Where(x => x.TitanID == id)
                .Select(y => new TitanImageViewModel
                {
                    TitanID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new TitanCreateViewModel();
            vm.ID = titan.ID;
            vm.TitanName = titan.TitanName;
            vm.TitanHealth = titan.TitanHealth;
            vm.TitanXP = titan.TitanXP;
            vm.TitanXPNextLevel = titan.TitanXPNextLevel;
            vm.TitanLevel = titan.TitanLevel;
            vm.TitanType = (Models.Titans.TitanType)titan.TitanType;
            vm.TitanStatus = (Models.Titans.TitanStatus)titan.TitanStatus;
            vm.PrimaryAttackName = titan.PrimaryAttackName;
            vm.PrimaryAttackPower = titan.PrimaryAttackPower;
            vm.SecondaryAttackName = titan.SecondaryAttackName;
            vm.SecondaryAttackPower = titan.SecondaryAttackPower;
            vm.SpecialAttackName = titan.SpecialAttackName;
            vm.SpecialAttackPower = titan.SpecialAttackPower;
            vm.TitanDied = titan.TitanDied;
            vm.TitanWasBorn = titan.TitanWasBorn;
            vm.CreatedAt = titan.CreatedAt;
            vm.UpdatedAt = DateTime.Now;
            vm.Image.AddRange(images);

            return View("Update", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TitanCreateViewModel vm)
        {
            var dto = new TitanDto()
            {
                ID = (Guid)vm.ID,
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
                CreatedAt = vm.CreatedAt,
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
            var result = await _titansServices.Update(dto);

            if (result == null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return NotFound(); }

            var titan = await _titansServices.DetailsAsync(id);

            if (titan == null) { return NotFound(); };

            var images = await _context.FilesToDatabase
                .Where(x => x.TitanID == id)
                .Select( y => new TitanImageViewModel
                {
                    TitanID = y.ID,
                    ImageID = y.ID,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
            var vm = new TitanDeleteViewModel();

            vm.ID = titan.ID;
            vm.TitanName = titan.TitanName;
            vm.TitanHealth = titan.TitanHealth;
            vm.TitanXP = titan.TitanXP;
            vm.TitanXPNextLevel = titan.TitanXPNextLevel;
            vm.TitanLevel = titan.TitanLevel;
            vm.TitanType = (Models.Titans.TitanType)titan.TitanType;
            vm.TitanStatus = (Models.Titans.TitanStatus)titan.TitanStatus;
            vm.PrimaryAttackName = titan.PrimaryAttackName;
            vm.PrimaryAttackPower = titan.PrimaryAttackPower;
            vm.SecondaryAttackName = titan.SecondaryAttackName;
            vm.SecondaryAttackPower = titan.SecondaryAttackPower;
            vm.SpecialAttackName = titan.SpecialAttackName;
            vm.SpecialAttackPower = titan.SpecialAttackPower;
            vm.CreatedAt = titan.CreatedAt;
            vm.UpdatedAt = DateTime.Now;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var titanToDelete = await _titansServices.Delete(id);

            if (titanToDelete == null) { return RedirectToAction("Index"); }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(TitanImageViewModel vm)
        {
            var dto = new FileToDatabaseDto()
            {
                ID = vm.ImageID
            };
            var image = await _fileServices.RemoveImageFromDatabase(dto);
            if (image == null) { return RedirectToAction("Index"); }
            return RedirectToAction("Index");
        }

        /*

        T I T A N O W N E R S H I P 

         */

        /// <summary>
        /// player get titan from story event
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost, ActionName("CreateTitanOwnership")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRandomTitanOwnership(TitanOwnershipFromStoryViewModel vm)
        {
            // create random int based on how many titans are in the database
            int RNG = new Random().Next(1, _context.Titans.Count());

            //find the source titan, based on random integer
            var sourceTitan = _context.Titans.OrderByDescending(x => x.TitanName).Take(RNG);

            var dto = new TitanOwnershipDto()
            {
                TitanName = vm.AddedTitan.TitanName,
                TitanHealth = 100,
                TitanXP = 0,
                TitanXPNextLevel = 100,
                TitanLevel = 0,
                TitanType = (Core.Dto.TitanType)vm.AddedTitan.TitanType,
                TitanStatus = (Core.Dto.TitanStatus)vm.AddedTitan.TitanStatus,
                PrimaryAttackName = vm.AddedTitan.PrimaryAttackName,
                PrimaryAttackPower = vm.AddedTitan.PrimaryAttackPower,
                SecondaryAttackName = vm.AddedTitan.SecondaryAttackName,
                SecondaryAttackPower = vm.AddedTitan.SecondaryAttackPower,
                SpecialAttackName = vm.AddedTitan.SpecialAttackName,
                SpecialAttackPower = vm.AddedTitan.SpecialAttackPower,
                TitanWasBorn = vm.AddedTitan.TitanWasBorn,
                OwnershipCreatedAt = DateTime.Now,
                OwnershipUpdatedAt = DateTime.Now,
                Files = vm.AddedTitan.Files,
                Image = vm.AddedTitan.Image 
                //.Select(x => new FileToDatabase
                //{
                //    ID = x.ImageID,
                //    ImageData = x.ImageData,
                //    ImageTitle = x.ImageTitle,
                //    TitanID = x.TitanID,
                //}).ToArray()
            };
            //var result = await _storiesServices.Create(dto);
            //STUB, needs storiesservices, a story to utilise, storiescontroller to function

            string result = null;
            if (result == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", vm);
        }

        [ValidateAntiForgeryToken]
        public async Task<TitanOwnership> NewRandomTitanOwnership(TitanOwnership newOwnership)
        {
            // create random int based on how many titans are in the database
            int RNG = new Random().Next(1, _context.Titans.Count());

            //find the source titan, based on random integer
            var sourceTitan = _context.Titans.OrderByDescending(x => x.TitanName).Take(RNG);

            var randomtitan = new TitanOwnership()
            {
                TitanName = newOwnership.TitanName,
                TitanHealth = 100,
                TitanXP = 0,
                TitanXPNextLevel = 100,
                TitanLevel = 0,
                TitanType = (Core.Dto.TitanType)newOwnership.TitanType,
                TitanStatus = (Core.Dto.TitanStatus)newOwnership.TitanStatus,
                PrimaryAttackName = newOwnership.PrimaryAttackName,
                PrimaryAttackPower = newOwnership.PrimaryAttackPower,
                SecondaryAttackName = newOwnership.SecondaryAttackName,
                SecondaryAttackPower = newOwnership.SecondaryAttackPower,
                SpecialAttackName = newOwnership.SpecialAttackName,
                SpecialAttackPower = newOwnership.SpecialAttackPower,
                TitanWasBorn = newOwnership.TitanWasBorn,
                OwnershipCreatedAt = DateTime.Now,
                OwnershipUpdatedAt = DateTime.Now,
                //Files = newOwnership.Files,
                //Image = newOwnership.Image
                //.Select(x => new FileToDatabase
                //{
                //    ID = x.ImageID,
                //    ImageData = x.ImageData,
                //    ImageTitle = x.ImageTitle,
                //    TitanID = x.TitanID,
                //}).ToArray()
            };
            await _titansServices.CreateRandom(randomtitan);

            //var result = await _storiesServices.Create(dto);
            //STUB, needs storiesservices, a story to utilise, storiescontroller to function

            return randomtitan;
        }

    }
}
