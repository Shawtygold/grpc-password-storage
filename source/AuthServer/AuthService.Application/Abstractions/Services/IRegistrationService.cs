namespace AuthService.Application.Abstractions.Services
{
    public interface IRegistrationService
    {
        Task<Guid> RegisterAsync(string login, string email, string password, CancellationToken cancellation = default);
    }
}
