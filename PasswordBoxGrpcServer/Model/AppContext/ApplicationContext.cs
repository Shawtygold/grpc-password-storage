using Microsoft.EntityFrameworkCore;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Model.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
        }
    }
    }
}
