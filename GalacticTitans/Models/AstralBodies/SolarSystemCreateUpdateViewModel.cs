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
        public List<Guid>? AstralBodyIDs { get; set; } = new List<Guid>(); 
        public List<AstralBodyIndexViewModel>? Planets { get; set; } = new(); 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
