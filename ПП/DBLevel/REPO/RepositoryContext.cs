using Microsoft.EntityFrameworkCore;

namespace REPO
{
    public class RepositoryContext : DbContext
    {
        public DbSet<WSRef> WSRefs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //связи между таблицами
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WSref)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.WSrefId);
        }
    }
}
