using GalacticTitans.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticTitans.Data
{
    public class GalacticTitansContext : DbContext
    {
        public GalacticTitansContext(DbContextOptions<GalacticTitansContext> options) : base(options) {}
        public DbSet<Titan> Titans { get; set; }
        public DbSet<AstralBody> AstralBodies { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
    }
}
