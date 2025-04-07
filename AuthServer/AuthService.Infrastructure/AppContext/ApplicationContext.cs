using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public ApplicationContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
            Database.EnsureCreated();
        }
    }
}
