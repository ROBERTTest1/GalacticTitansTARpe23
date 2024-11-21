using GalacticTitans.Core.Domain;
using GalacticTitans.Models.Titans;
using TitanType = GalacticTitans.Models.Titans.TitanType;

namespace GalacticTitans.Models.AstralBodies
{
    public class AstralBodyIndexViewModel
    {
        public Guid ID { get; set; }
        public string AstralBodyName { get; set; }
        public AstralBodyType AstralBodyType { get; set; }
        public TitanType EnvironmentBoost { get; set; }
        public int MajorSettlements { get; set; }
        public KardashevScale TechnicalLevel { get; set; }
        //public Guid PlayerProfileID { get; set; }
        public Guid SolarSystemID { get; set; }

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
