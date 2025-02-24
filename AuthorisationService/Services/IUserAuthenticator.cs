using AuthService.Model.Entities;

namespace AuthService.Services
{
    public interface IUserAuthenticator
    {
        Task<User?> AuthenticateAsync(User user);
    }
}
