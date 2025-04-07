namespace AuthService.Infrastructure.Abstractions.Providers
{
    public interface IJWTProvider
    {
        public string GetJWTToken(Guid userId);
    }
}
