namespace AuthService.Application.Abstractions.Services
{
    public interface ILoginService
    {
        Task<string> LoginAsync(string login, string password, CancellationToken cancellation = default);
    }
}
