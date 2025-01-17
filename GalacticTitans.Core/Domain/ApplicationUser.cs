using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
        public Guid PlayerProfileID { get; set; } //1-1
        public bool ProfileType { get; set; } //true, admin, false, player
    }
}
