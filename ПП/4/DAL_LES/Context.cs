using DAL_LES.Interfaces;
using DAL_LES.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL_LES { 

public class SQLContext : DbContext
{
    public DbSet<Celebrity> Celebrities { get; set; }
    public DbSet<LifeEvent> LifeEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Ты хочешь мою строку подключения? А я не хочу :3");
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LifeEvent>()
                .HasOne(le => le.Celebrity)
                .WithMany(c => c.Lifeevents)
                .HasForeignKey(le => le.CelebrityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public SQLContext() : base()
        {
            Database.EnsureCreated();
        }
}
}