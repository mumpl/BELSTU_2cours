// Context.cs
using Microsoft.EntityFrameworkCore;
using REPO;

namespace DALMSQLX
{
    public class Context : DbContext
    {

        public Context() : base()
        {
            Database.EnsureCreated(); //делает пометку что необходимо создать БД если её нет. Она создаётся ленивым способом, то есть при первом обращении к бд
        }
        public DbSet<WSRef> WSRefs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=USER-PC;Database=Patterns;Trusted_Connection=True;Encrypt=False;"); //отключил шифрование
            //optionsBuilder.UseSqlServer(@"Data source=172.16.193.88; Initial Catalog=DAL21;TrustServerCertificate=True;User Id=smw60;Password=21625");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WSref)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.WSrefId);
        }
    }
}