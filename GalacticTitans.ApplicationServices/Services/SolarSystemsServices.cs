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
        private readonly IAstralBodiesServices _astralBodiesServices;

        public SolarSystemsServices(GalacticTitansContext context, IFileServices fileServices, IAstralBodiesServices astralBodiesServices)
        {
            _context = context;
            _fileServices = fileServices;
            _astralBodiesServices = astralBodiesServices;
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
            newSystem.AstralBodyIDs = PlanetToID(planetsInSystem);
            /*opcheck: newsystem is not assigned any planetids*/
            await _context.SolarSystems.AddAsync(newSystem);
            await _context.SaveChangesAsync();

            foreach (var planet in planetsInSystem)
            {
                _context.AstralBodies.Attach(planet);
                planet.SolarSystemID = newSystem.ID;
                planet.ModifiedAt = DateTime.Now;

                _context.Entry(planet).Property(p => p.SolarSystemID).IsModified = true;
                _context.Entry(planet).Property(p => p.ModifiedAt).IsModified = true;

                /*opfail: second object cannot be updated to have the same solarsystemid for some reason*/
                //_context.AstralBodies.Attach(planet).Property(ssid => ssid.SolarSystemID).IsModified = true;
                //_context.AstralBodies.Attach(planet).Property(mfat => mfat.ModifiedAt).IsModified = true;
                
                
                await _context.SaveChangesAsync();
            }

            return newSystem;
        }
        public async Task<SolarSystem> Update(SolarSystemDto dto, List<AstralBody> planetsInSystem, List<AstralBody> removedPlanets)
        {
            SolarSystem modifiedSystem = new();

            // set by service on first creation
            modifiedSystem.ID = (Guid)dto.ID;

            // set by admin
            modifiedSystem.SolarSystemName = dto.SolarSystemName;
            modifiedSystem.SolarSystemLore = dto.SolarSystemLore;
            modifiedSystem.AstralBodyAtCenter = dto.AstralBodyAtCenter;
            // set for db
            modifiedSystem.CreatedAt = DateTime.Now;
            modifiedSystem.UpdatedAt = DateTime.Now;
            modifiedSystem.AstralBodyIDs = PlanetToID(planetsInSystem);
            /*opcheck: newsystem is not assigned any planetids*/
            _context.SolarSystems.Update(modifiedSystem);
            await _context.SaveChangesAsync();

            foreach (var planet in planetsInSystem)
            {
                _context.AstralBodies.Attach(planet);
                planet.SolarSystemID = modifiedSystem.ID;
                planet.ModifiedAt = DateTime.Now;

                _context.Entry(planet).Property(p => p.SolarSystemID).IsModified = true;
                _context.Entry(planet).Property(p => p.ModifiedAt).IsModified = true;

                /*opfail: second object cannot be updated to have the same solarsystemid for some reason*/
                //_context.AstralBodies.Attach(planet).Property(ssid => ssid.SolarSystemID).IsModified = true;
                //_context.AstralBodies.Attach(planet).Property(mfat => mfat.ModifiedAt).IsModified = true;


                await _context.SaveChangesAsync();
            }
            // null ids of planets kicked out
            foreach (var planet in removedPlanets)
            {
                _context.AstralBodies.Attach(planet);
                planet.SolarSystemID = null;
                planet.ModifiedAt = DateTime.Now;

                _context.Entry(planet).Property(p => p.SolarSystemID).IsModified = true;
                _context.Entry(planet).Property(p => p.ModifiedAt).IsModified = true;

                await _context.SaveChangesAsync();
            }

            return modifiedSystem;
        }
        private static List<Guid> PlanetToID(List<AstralBody> planets)
        {
            var result = new List<Guid>();
            foreach (var planet in planets)
            {
                result.Add(planet.ID);
            }
            return result;
        }
    }
}
