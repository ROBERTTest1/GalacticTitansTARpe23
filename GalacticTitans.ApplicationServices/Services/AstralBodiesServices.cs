using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.ApplicationServices.Services
{
    public class AstralBodiesServices : IAstralBodiesServices
    {
        private readonly GalacticTitansContext _context;
        private readonly IFileServices _fileServices;

        public AstralBodiesServices(GalacticTitansContext context , IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }
        public async Task<AstralBody> DetailsAsync(Guid id)
        {
            var result = await _context.AstralBodies
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<AstralBody> Create(AstralBodyDto dto)
        {
            Random RNG = new Random(); // make a random rng 
            int settlementcount; //make variable called settlementcount, it is of type int
            if (dto.MajorSettlements == null) //iif user has not input a settlementcount
            { settlementcount = RNG.Next(1, 200); } //it will be randomly generated
            else { settlementcount = dto.MajorSettlements; } //otherwise, settlementcount is what it is

            AstralBody newPlanet = new();

            // set by service on first creation
            newPlanet.ID = Guid.NewGuid();
            newPlanet.MajorSettlements = settlementcount;
            if (settlementcount >= 189) 
            { newPlanet.TechnicalLevel = KardashevScale.Type3; }
            else if (190 >= settlementcount && settlementcount >= 150) 
            { newPlanet.TechnicalLevel = KardashevScale.Type2; }
            else { newPlanet.TechnicalLevel = KardashevScale.Type1; }
            //no titan owns a planet that has been created by admin
            //and no planet is assigned to a solar system in the planet creation view

            // set by admin
            newPlanet.AstralBodyName = dto.AstralBodyName;
            newPlanet.AstralBodyType = dto.AstralBodyType;
            newPlanet.EnvironmentBoost = (Core.Domain.TitanType)dto.EnvironmentBoost;
            newPlanet.AstralBodyDescription = dto.AstralBodyDescription;

            // set for db
            newPlanet.CreatedAt = DateTime.Now;
            newPlanet.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, newPlanet);
            }

            await _context.AstralBodies.AddAsync(newPlanet);
            await _context.SaveChangesAsync();
            return newPlanet;
        }


    }
}
