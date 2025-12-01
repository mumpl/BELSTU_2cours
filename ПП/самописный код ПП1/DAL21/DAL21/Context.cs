using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DAL21
{ 
    public class Context : DbContext
    {
        public Context() : base()
        {
            Database.EnsureCreated(); //делает пометку что необходимо создать БД если её нет. Она создаётся ленивым способом, то есть при первом обращении к бд
        }
        public DbSet<WSRef>   WSRefs   { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=USER-PC;Database=Patterns;Trusted_Connection=True;Encrypt=False;"); 
            //optionsBuilder.UseSqlServer(@"Data source=172.16.193.88; Initial Catalog=DAL21;TrustServerCertificate=True;User Id=smw60;Password=21625");
        }    
    }
}

//new SqlServerDbContextOptionsExtensions.UseSqlServer    ("xx") 
////DbContextOptions<Context> options = new DbContextOptions<Context>();
//public Context(DbContextOptions<Context> options) : base(options)
//optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True; Trusted_Connection=True;");
//optionsBuilder.UseSqlServer(@"Server=172.16.193.88;Database=WXXX;MultipleActiveResultSets=true;Trusted_Connection=True;User Id=nnnnnnnnnnnnn;Password=mmmmm");