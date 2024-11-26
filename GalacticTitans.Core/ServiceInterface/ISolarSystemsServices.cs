using GalacticTitans.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface ISolarSystemsServices
    {
        Task<SolarSystem> DetailsAsync(Guid id);
    }
}
