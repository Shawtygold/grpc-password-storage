using AuthService.Model.Entities;

namespace AuthService.Services
{
    public interface IUserRegistration
    {
        Task<User> RegisterAsync(User user);
    }
}
