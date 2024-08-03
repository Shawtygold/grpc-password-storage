using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRegistration _userRegistration;
        private readonly IUserCreator _userCreator;
        private readonly IUserAuthenticator _userAuthenticator;

        public UserService(IUserRegistration registrationService,
            IUserCreator userCreatorService,
            IUserAuthenticator userAuthenticationService)
        {
            _userRegistration = registrationService;
            _userCreator = userCreatorService;
            _userAuthenticator = userAuthenticationService;
        }
        public async Task<bool> AuthenticateAsync(User user)
        {
            return await _userAuthenticator.AuthenticateAsync(user);
        }

        public User Create(string login, string password)
        {
            return _userCreator.Create(login, password);
        }

        public async Task RegisterAsync(User user)
        {
            await _userRegistration.RegisterAsync(user);
        }

        public void Dispose()
        {
            _userRegistration?.Dispose();
            _userAuthenticator?.Dispose();
        }
    }
}
