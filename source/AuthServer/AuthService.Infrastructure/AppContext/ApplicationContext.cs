using AuthService.Domain.Entities;
using AuthService.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        private readonly IOptions<RefreshTokenDbSettings> _options;
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationContext(IOptions<RefreshTokenDbSettings> options)
        {
            _options = options;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_options.Value.ConnectionString);
        }
    }
}
