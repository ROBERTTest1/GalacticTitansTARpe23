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
    public class AstralBodiesServices : IAstralBodiesServices
    {
        private readonly GalacticTitansContext _context;
        //private readonly IFileServices _fileServices;

        public AstralBodiesServices(GalacticTitansContext context /*, IFileServices fileServices*/)
        {
            _context = context;
            //_fileServices = fileServices
        }
        public async Task<AstralBody> DetailsAsync(Guid id)
        {
            var result = await _context.AstralBodies
                .FirstOrDefaultAsync(x => x.ID == id);
            return result;
        }




    }
}
