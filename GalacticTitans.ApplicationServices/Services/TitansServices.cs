using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using Microsoft.EntityFrameworkCore;

namespace GalacticTitans.ApplicationServices.Services
{
    public class TitansServices : ITitansServices
    {
        private readonly GalacticTitansContext _context;
        private readonly IFileServices _fileServices;

        public TitansServices(GalacticTitansContext context , IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        /// <summary>
        /// Get Details for one Titan
        /// </summary>
        /// <param name="id">Id of titan to show details of</param>
        /// <returns>resulting titan</returns>
        public async Task<Titan> DetailsAsync(Guid id)
        {
            var result = await _context.Titans
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<Titan> Create(TitanDto dto)
        {
            Titan titan = new Titan();

            // set by service
            titan.ID = Guid.NewGuid();
            titan.TitanHealth = 100;
            titan.TitanXP = 0;
            titan.TitanXPNextLevel = 100;
            titan.TitanLevel = 0;
            titan.TitanStatus = Core.Domain.TitanStatus.Alive;
            titan.TitanWasBorn = DateTime.Now;
            titan.TitanDied = DateTime.Parse("01/01/9999 00:00:00");

            //set by user
            titan.TitanType = (Core.Domain.TitanType)dto.TitanType;
            titan.PrimaryAttackName = dto.PrimaryAttackName;
            titan.PrimaryAttackPower = dto.PrimaryAttackPower;
            titan.SecondaryAttackName = dto.SecondaryAttackName;
            titan.SecondaryAttackPower = dto.SecondaryAttackPower;
            titan.SpecialAttackName = dto.SpecialAttackName;
            titan.SpecialAttackPower = dto.SpecialAttackPower;

            //set for db
            titan.CreatedAt = DateTime.Now;
            titan.UpdatedAt = DateTime.Now;

            //files
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, titan);
            }

            await _context.Titans.AddAsync(titan);
            await _context.SaveChangesAsync();

            return titan;
        }
    }
}
