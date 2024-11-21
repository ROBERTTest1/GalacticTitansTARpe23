using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Domain
{
    public enum AstralBodyType
    {
        BlackHole_SuperMassive,
        BlackHole_Common,
        Blackhole_Small,
        Star_RedGiant,
        Star_Common,
        Star_WhiteDwarf,
        Star_Neutron,
        Star_RedDwarf,
        Star_BrownDwarf,
        DebrisCloud,
        DysonSphere,
        Planet_GasGiant,
        Planet_IceGiant,
        Planet_RockGiant,
        Planet_LargeGasIce,
        Planet_Gas,
        Planet_GasRock,
        Planet_MegaTerrestrial,
        Planet_Terrestrial,
        Planet_Dwarf,
        Moon_Giant,
        Moon_Lunar,
        Moon_GravLockObj,
        Asteroid,
        Meteor,
        Comet,
        Satellite
    }
    public enum KardashevScale
    {
        Type1,Type2,Type3
    }
    public class AstralBody
    {
        public Guid ID { get; set; }
        public string AstralBodyName { get; set; }
        public AstralBodyType AstralBodyType { get; set; }
        public TitanType EnvironmentBoost { get; set; }
        public string AstralBodyDescription { get; set; }
        public int MajorSettlements { get; set; }
        public KardashevScale TechnicalLevel { get; set; }
        //public List<Titan> TitansWhoOwnThisPlanet { get; set; }
        public Titan TitanWhoOwnsThisPlanet { get; set; }
        //public Guid PlayerProfileID { get; set; }
        public Guid SolarSystemID { get; set; }

        //db
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
