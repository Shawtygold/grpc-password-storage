namespace AuthService.Infrastructure.Settings
{
    public sealed class RefreshTokenDbSettings
    {
        public const string SectionName = "RefreshTokenDbSettings";
        public string PostgreSQLConnection { get; set; } = null!;
    }
}
