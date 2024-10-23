using AuthorisationService.Model.Entities;

namespace AuthorisationService.Interfaces.Services
{
    public interface IUserAuthenticator
    {
        Task<bool> AuthenticateAsync(User user);
    }
}
