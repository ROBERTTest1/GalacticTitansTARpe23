using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Domain
{
    public enum ProfileStatus
    {
        Active,Abandoned,Deactivated,Locked,Banned,ManualReviewNecessary
    }
    public class PlayerProfile
    {
        public Guid ID { get; set; }
        public string ApplicationUserID { get; set; } // 1-1
        public string ScreenName { get; set; }
        public int GalacticCredits { get; set; }
        public int ScrapResource { get; set; }
        public List<TitanOwnership> MyTitans { get; set; }
        public int Victories { get; set; }
        public string? MySolarSystem { get; set; } //do not use this property yet, this needs to become a solarsystem object attached to the player, this is not a system the admin makes, in the future, it will be part of feature for creating users own solar system
        public ProfileStatus CurrentStatus { get; set; }

        public bool ProfileType { get; set; } //true, admin, false, player

        //dbonly
        public DateTime ProfileCreatedAt { get; set; }
        public DateTime ProfileModifiedAt { get; set; }
        public DateTime ProfileAttributedToAnAccountUserAt { get; set; }
        public DateTime ProfileStatusLastChangedAt { get; set; }

    }
}
