using AuthorisationService.Model.Entities;

namespace AuthorisationService.Services
{
    public interface IUserAuthenticator
    {
        Task<User?> AuthenticateAsync(User user);
    }
}
