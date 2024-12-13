using AuthorisationService.Model.Entities;

namespace AuthorisationService.Interfaces.Services
{
    public interface IUserAuthenticator
    {
        Task<User?> AuthenticateAsync(User user);
    }
}
