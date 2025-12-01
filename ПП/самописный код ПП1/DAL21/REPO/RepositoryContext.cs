using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация отношений
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.WSref)
                .WithMany(w => w.Comments)
                .HasForeignKey(c => c.WSrefId);
        }
    }
}
