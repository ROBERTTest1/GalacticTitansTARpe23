using GalacticTitans.Core.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Core.Dto
{
    public class AstralBodyDto
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

        //image 
        public List<IFormFile> Files { get; set; }
        public IEnumerable<FileToDatabaseDto> Image { get; set; } = new List<FileToDatabaseDto>();

        //db
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
