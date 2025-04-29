namespace AuthService.Infrastructure.Settings
{
    public class JWTSettings
    {
        public const string SectionName = "JWTSettings";
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudence { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string LifetimeInMinutes { get; set; } = null!;
    }
}
