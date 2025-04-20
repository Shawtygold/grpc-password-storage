namespace AuthService.Application.Abstractions.Providers
{
    public interface IJWTProvider
    {
        public string GetJWTToken(Guid userId);
    }
}
