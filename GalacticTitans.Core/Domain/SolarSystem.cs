using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Domain
{
    public class SolarSystem
    {
        public Guid ID { get; set; }
        public Guid AstralBodyAtCenter { get; set; }
        public AstralBody? AstralBodyAtCenterWith { get; set; }
        public string SolarSystemName { get; set; }
        public string SolarSystemLore { get; set; }
        //public Guid ControllingPlayerID { get; set; }
        public List<Guid> AstralBodyIDs { get; set; } = new List<Guid>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
