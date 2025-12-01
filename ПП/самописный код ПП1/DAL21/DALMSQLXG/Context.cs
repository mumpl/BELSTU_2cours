using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace DALMSQLXG
{
    public class DALMSQLXGContext : DbContext
    {
        public DbSet<WSref> WSrefs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DALMSQLXGContext(DbContextOptions<DALMSQLXGContext> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WSref>()
                .HasMany(w => w.Comments)
                .WithOne(c => c.WSref)
                .HasForeignKey(c => c.WSrefId);
        }
    }
}