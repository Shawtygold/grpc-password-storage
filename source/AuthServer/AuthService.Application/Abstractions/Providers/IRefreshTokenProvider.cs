namespace AuthService.Application.Abstractions.Providers
{
    public interface IRefreshTokenProvider
    {
        string GetAccessToken(Guid userId);
        string GenerateRefreshToken();
    }
}
