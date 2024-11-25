using AuthorisationService.Model.Entities;

namespace AuthorisationService.Interfaces.Services
{
    public interface IUserRegistration
    {
        Task<User> RegisterAsync(User user);
    }
}
