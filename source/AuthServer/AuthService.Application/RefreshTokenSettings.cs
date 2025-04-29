namespace AuthService.Application
{
    public class RefreshTokenSettings
    {
        public const string SectionName = "RefreshTokenSettings";
        public string LifetimeInDays { get; set; } = null!;
    }
}
