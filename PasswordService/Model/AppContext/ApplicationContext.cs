using Microsoft.EntityFrameworkCore;
using PasswordService.Model.Entities;

namespace PasswordService.Model.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Password> Passwords => Set<Password>();

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
