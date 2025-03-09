using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PasswordService.Domain.Entities;

namespace PasswordService.Infrastructure.AppContext
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Password> Passwords => Set<Password>();
        private readonly string _connectionString;

        public ApplicationContext(IOptions<AppContextSettings> _settings)
        {
            _connectionString = !string.IsNullOrEmpty(_settings.Value.ConnectionString) 
                ? _settings.Value.ConnectionString : throw new ArgumentException("'ConnectionString' cannot be empty");

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }  
    }

    public class AppContextSettings
    {
        public const string SectionName = "SqliteSettings";
        public string ConnectionString { get; set; } = null!;
    }
}
