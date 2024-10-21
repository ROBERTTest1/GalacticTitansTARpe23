using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Dto
{
    public enum TitanType
    {
        Earth, Solid, Digital, BlackHole, Liquid, Gas, Plasma, Nebulous, Entity, Humanoid, Demonic, AlternateDimension, Unknown
    }
    public enum TitanStatus
    {
        Dead, Alive, GuardingPlanet
    }
    public class TitanDto
    {
        public Guid ID { get; set; }
        public string TitanName { get; set; }
        public int TitanHealth { get; set; }
        public int TitanXP { get; set; }
        public int TitanXPNextLevel { get; set; }
        public int TitanLevel { get; set; }
        public TitanType TitanType { get; set; }
        public TitanStatus TitanStatus { get; set; }
        public int PrimaryAttackPower { get; set; }
        public string PrimaryAttackName { get; set; }
        public int SecondaryAttackPower { get; set; }
        public string SecondaryAttackName { get; set; }
        public int SpecialAttackPower { get; set; }
        public string SpecialAttackName { get; set; }
        public DateTime TitanWasBorn { get; set; }
        public DateTime TitanDied { get; set; }

        //image 
               
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();
        

        //db only
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
