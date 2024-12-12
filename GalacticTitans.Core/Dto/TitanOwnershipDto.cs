using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Dto
{
    public class TitanOwnershipDto : TitanDto
    {
        public Guid OwnershipID { get; set; }
        public int TitanHealth { get; set; }
        public int TitanXP { get; set; }
        public int TitanXPNextLevel { get; set; }
        public int TitanLevel { get; set; }
        public TitanStatus TitanStatus { get; set; }
        public int PrimaryAttackPower { get; set; }
        public int SecondaryAttackPower { get; set; }
        public int SpecialAttackPower { get; set; }
        public DateTime TitanWasBorn { get; set; }
        public DateTime TitanDied { get; set; }
        //public string OwnedByPlayerProfile { get; set; } //is string but holds guid

        //db only
        public DateTime OwnershipCreatedAt { get; set; }
        public DateTime OwnershipUpdatedAt { get; set; }
    }
}
