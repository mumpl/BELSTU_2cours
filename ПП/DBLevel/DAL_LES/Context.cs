using Microsoft.EntityFrameworkCore;

namespace DAL_LES
{
    public class Context : DbContext
    {
        public Context() : base()
        {
            Database.EnsureCreated();
        }
        public DbSet<Lifeevent>? Lifeevents { get; set; }
        public DbSet<Celebrity>? Celebrities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP; Initial Catalog=LES; TrustServerCertificate=True; Integrated Security=True;");
        }
    }
}
