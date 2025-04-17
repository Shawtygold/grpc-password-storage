namespace PasswordService.Infrastructure
{
    public class JWTStructure
    {
        public string ValidIssuer { get; set; } = null!;
        public string ValidAudence { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
    }
}
