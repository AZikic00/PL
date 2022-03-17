using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Context : DbContext
    {
        public DbSet<Klub> Klubovi { get; set; }
        public DbSet<Igrac> Igraci { get; set; }
        public DbSet<Sudija> Sudije { get; set; }
        public DbSet<Sezona> Sezone { get; set; }
        public DbSet<Utakmica> Utakmice { get; set; }     

        public Context(DbContextOptions options):base(options) {}
    }
}