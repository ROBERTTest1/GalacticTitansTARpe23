using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface IAstralBodiesServices
    {
        Task<AstralBody> DetailsAsync(Guid id);
        Task<AstralBody> Create(AstralBodyDto dto);
        Task<AstralBody> Delete(Guid id);
    }
}
