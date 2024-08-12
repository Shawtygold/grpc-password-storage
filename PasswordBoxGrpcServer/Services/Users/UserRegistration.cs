using Grpc.Core;
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

        public async Task<bool> RegisterAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            user.Login = await _encryptor.EncryptAsync(user.Login);
            user.Password = await _encryptor.EncryptAsync(user.Password);

            if (await _userRepository.GetByAsync(u => u.Login == user.Login) != null)
                throw new RpcException(new Status(StatusCode.AlreadyExists, "User with that login already exists"));

            await _userRepository.AddAsync(user);
            return true;
        }
    }
}
