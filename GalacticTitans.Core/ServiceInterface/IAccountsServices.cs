using GalacticTitans.Core.Domain;
using GalacticTitans.Core.Dto.AccountsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.ServiceInterface
{
    public interface IAccountsServices
    {
        Task<ApplicationUser> ConfirmEmail(string userId, string token);
        Task<ApplicationUser> Register(ApplicationUserDto dto);

    }
}
