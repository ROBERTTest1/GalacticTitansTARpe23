using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Domain
{
    public class Galaxy
    {
        public Guid ID { get; set; }
        public string GalaxyName { get; set; }
        public string GalaxyLore { get; set; }
        public List<Guid> SolarSystemsInGalaxy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
