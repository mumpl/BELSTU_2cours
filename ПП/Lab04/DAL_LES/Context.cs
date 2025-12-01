using Microsoft.EntityFrameworkCore;

namespace DAL_LES
{
    public class Context : DbContext
    {
        public DbSet<Celebrity> Celebrities { get; set; }
        public DbSet<Lifeevent> Lifeevents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Initial Catalog=DAL_LES;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Celebrity>()
                .HasMany(c => c.Lifeevents)
                .WithOne(e => e.Celebrity)
                .HasForeignKey(e => e.CelebrityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
