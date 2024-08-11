using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRegistration _userRegistration;
        private readonly IUserAuthenticator _userAuthenticator;

        public UserService(IUserRegistration registrationService,
            IUserAuthenticator userAuthenticationService)
        {
            _userRegistration = registrationService;
            _userAuthenticator = userAuthenticationService;
        }
        public async Task<bool> AuthenticateAsync(User user)
        {
            return await _userAuthenticator.AuthenticateAsync(user);
        }

        public async Task RegisterAsync(User user)
        {
            await _userRegistration.RegisterAsync(user);
        }
    }
}
