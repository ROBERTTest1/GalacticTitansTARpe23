using GalacticTitans.Core.Domain;

namespace GalacticTitans.Models.Profiles
{
    public class ProfileRegisterViewModel
    {
        public Guid ID { get; set; }
        public string ApplicationUserID { get; set; } // 1-1
        public string ScreenName { get; set; }
        public int GalacticCredits { get; set; }
        public int ScrapResource { get; set; }
        public List<TitanOwnership> MyTitans { get; set; }
        public int Victories { get; set; }
        public int MyProperty { get; set; }
        public string? MySolarSystem { get; set; } //do not use this property yet, this needs to become a solarsystem object attached to the player, this is not a system the admin makes, in the future, it will be part of feature for creating users own solar system
        public ProfileStatus CurrentStatus { get; set; }

        public bool ProfileType { get; set; } //true, admin, false, player
    }
}
