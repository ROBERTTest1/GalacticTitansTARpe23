using GalacticTitans.Core.Domain;

namespace GalacticTitans.Models.AstralBodies
{
    public class SolarSystemCreateUpdateViewModel
    {
        public Guid? ID { get; set; }
        public Guid AstralBodyAtCenter { get; set; }
        public AstralBody? AstralBodyAtCenterWith { get; set; }
        public string SolarSystemName { get; set; }
        public string SolarSystemLore { get; set; }
        //public Guid ControllingPlayerID { get; set; }
        public List<Guid>? AstralBodyIDs { get; set; } = new List<Guid>(); //set as optional, but discarded in reality
        public List<AstralBodyIndexViewModel>? Planets { get; set; } = new(); //set as optional, but discarded in reality
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
