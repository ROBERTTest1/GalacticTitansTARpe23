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
    public class TitansServices : ITitansServices
    {
        private readonly GalacticTitansContext _context;
        //private readonly IFileServices _fileServices;

        public TitansServices(GalacticTitansContext context /*, IFileServices fileServices*/)
        {
            _context = context;
            //_fileServices = fileServices
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
    }
}
