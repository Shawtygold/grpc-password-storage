using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Users
{
    public class UserRegistration : IUserRegistration
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;

        public UserRegistration(IUserRepository userRepository, IEncryptor encryptor)
        {
            _userRepository = userRepository;
            _encryptor = encryptor;
        }

        public async Task RegisterAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            user.Login = await _encryptor.EncryptAsync(user.Login);
            user.Password = await _encryptor.EncryptAsync(user.Password);

            await _userRepository.AddAsync(user);
        }
    }
}
