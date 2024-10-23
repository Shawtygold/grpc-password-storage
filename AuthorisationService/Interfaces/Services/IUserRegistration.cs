using AuthorisationService.Model.Entities;

namespace AuthorisationService.Interfaces.Services
{
    public interface IUserRegistration
    {
        Task<bool> RegisterAsync(User user);
    }
}
