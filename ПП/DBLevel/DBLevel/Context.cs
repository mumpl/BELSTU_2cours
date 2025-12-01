using Microsoft.EntityFrameworkCore;

namespace DBLevel
{
    internal class Context : DbContext
    {
        public Context() : base()
        {
            //существует ли бд. Если нет — создает
            Database.EnsureCreated();
        }
        public DbSet<WSRef>? WSRefs { get; set; } //коллекция, которая представляет таблицу
        public DbSet<Comment>? Comments { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //соед-е с бд
        {
            optionsBuilder.UseSqlServer(@"Data Source=LAPTOP; Initial Catalog=SSSS; TrustServerCertificate=True; Integrated Security=True;");
        }
    }
}
