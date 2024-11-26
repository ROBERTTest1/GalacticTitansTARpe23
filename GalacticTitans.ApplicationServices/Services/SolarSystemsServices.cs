using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using GalacticTitans.Core.ServiceInterface;
using GalacticTitans.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.ApplicationServices.Services
{
    public class SolarSystemsServices : ISolarSystemsServices
    {
        private readonly GalacticTitansContext _context;
        private readonly IFileServices _fileServices;
        private readonly IAstralBodiesServices _bodiesServices;

        public SolarSystemsServices(GalacticTitansContext context, IFileServices fileServices, IAstralBodiesServices bodiesServices)
        {
            _context = context;
            _fileServices = fileServices;
            _bodiesServices = bodiesServices;
        }

        public async Task<SolarSystem> DetailsAsync(Guid id)
        {
            var result = await _context.SolarSystems
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }

        public async Task<SolarSystem> Create(SolarSystemDto dto, List<AstralBody> planetsInSystem)
        {
            SolarSystem newSystem = new();

            // set by service on first creation
            newSystem.ID = Guid.NewGuid();

            // set by admin
            newSystem.SolarSystemName = dto.SolarSystemName;
            newSystem.SolarSystemLore = dto.SolarSystemLore;
            newSystem.AstralBodyAtCenter = dto.AstralBodyAtCenter;
            // set for db
            newSystem.CreatedAt = DateTime.Now;
            newSystem.UpdatedAt = DateTime.Now;

            await _context.SolarSystems.AddAsync(newSystem);
            await _context.SaveChangesAsync();

            foreach (var planetID in planetsInSystem)
            {

            }

            return newSystem;
        }
    }
}
