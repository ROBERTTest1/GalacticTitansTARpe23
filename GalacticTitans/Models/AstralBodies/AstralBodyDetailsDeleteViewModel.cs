using GalacticTitans.Core.Domain;

namespace GalacticTitans.Models.AstralBodies
{
    public class AstralBodyDetailsDeleteViewModel
    {
        public Guid? ID { get; set; }
        public string AstralBodyName { get; set; }
        public AstralBodyType AstralBodyType { get; set; }
        public TitanType EnvironmentBoost { get; set; }
        public string AstralBodyDescription { get; set; }
        public int MajorSettlements { get; set; }
        public KardashevScale TechnicalLevel { get; set; }
        //public List<Titan> TitansWhoOwnThisPlanet { get; set; }
        public Titan? TitanWhoOwnsThisPlanet { get; set; }
        //public Guid PlayerProfileID { get; set; }
        public string? SolarSystemID { get; set; }

        //image
        public List<AstralBodyImageViewModel> Image { get; set; } = new();

        //db
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
