using PasswordBoxGrpcServer.Interfaces;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services
{
    public sealed class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Authenticate(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            User? dbUser = await _userRepository.GetByAsync(u => u.Login == user.Login);

            if (dbUser == null)
                return false;

            if (user.Password != dbUser.Password)
                return false;

            return true;
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
