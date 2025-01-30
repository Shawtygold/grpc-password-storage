using AuthorisationService.Model.Entities;

namespace AuthorisationService.Services
{
    public interface IUserRegistration
    {
        Task<User> RegisterAsync(User user);
    }
}
