namespace AuthService.Application.Abstractions.Services
{
    public interface IUserAuthenticator
    {
        Task<string> AuthenticateAsync(string login, string password, CancellationToken cancellation = default);
    }
}
