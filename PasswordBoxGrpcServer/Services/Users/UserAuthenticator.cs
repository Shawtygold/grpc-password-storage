using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;

        public UserAuthenticator(IUserRepository userRepository, IEncryptor encryptor)
        {
            _userRepository = userRepository;
            _encryptor = encryptor;
        }

        public async Task<bool> AuthenticateAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            // Encryption
            user.Login = await _encryptor.EncryptAsync(user.Login);
            user.Password = await _encryptor.EncryptAsync(user.Password);

            User? dbUser = await _userRepository.GetByAsync(u => u.Login == user.Login);

            if (dbUser == null)
                return false;

            if (user.Password != dbUser.Password)
                return false;

            return true;
        }
    }
}
