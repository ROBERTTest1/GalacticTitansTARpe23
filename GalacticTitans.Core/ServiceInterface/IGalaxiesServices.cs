using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface IGalaxiesServices
    {
        Task<Galaxy> DetailsAsync(Guid id);
        Task<Galaxy> Create(GalaxyDto dto, List<SolarSystem> systemsInGalaxy);
        Task<Galaxy> Update(GalaxyDto dto, List<SolarSystem> systemsInGalaxy, List<SolarSystem> removedSystems);
    }
}
