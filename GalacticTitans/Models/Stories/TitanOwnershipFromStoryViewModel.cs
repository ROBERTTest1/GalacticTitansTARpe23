using GalacticTitans.Core.Dto;

namespace GalacticTitans.Models.Stories
{
    public class TitanOwnershipFromStoryViewModel
    {
        public string PlayerProfileGUID { get; set; }
        public string StoryGUID { get; set; }
        public TitanOwnershipDto AddedTitan { get; set; }
    }
}
