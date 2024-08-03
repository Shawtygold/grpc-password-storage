using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AuthenticateAsync(User user)
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
