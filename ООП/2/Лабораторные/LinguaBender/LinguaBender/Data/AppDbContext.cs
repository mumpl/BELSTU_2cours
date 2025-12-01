using LinguaBender.Models;
using Microsoft.EntityFrameworkCore;

namespace LinguaBender.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } //настройки контекста бд

        public DbSet<Course> Courses { get; set; }
    }
}
