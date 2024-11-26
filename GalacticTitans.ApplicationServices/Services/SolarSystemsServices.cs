using GalacticTitans.Core.Domain;
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

        public SolarSystemsServices(GalacticTitansContext context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<SolarSystem> DetailsAsync(Guid id)
        {
            var result = await _context.SolarSystems
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }
    }
}
