using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface ITitansServices
    {
        Task<Titan> DetailsAsync(Guid id);
        Task<Titan> Create(TitanDto dto);
        Task<Titan> Update(TitanDto dto);
    }
}
