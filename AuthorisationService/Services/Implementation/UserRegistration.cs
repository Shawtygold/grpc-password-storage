using AuthService.Model.Cryptographers;
using AuthService.Model.Entities;
using AuthService.Model.Repositories;
using Grpc.Core;

namespace AuthService.Services.Implementation
{
    public class UserRegistration : IUserRegistration
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;
        private readonly IEncryptionHelper _encryptionHelper;

        public UserRegistration(IUserRepository userRepository, IEncryptor encryptor, IEncryptionHelper encryptionHelper)
        {
            _userRepository = userRepository;
            _encryptor = encryptor;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<User> RegisterAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            await _encryptionHelper.EncryptAsync(_encryptor, user);

            if (await _userRepository.GetByAsync(u => u.Login == user.Login) != null)
                throw new RpcException(new Status(StatusCode.AlreadyExists, "User with that login already exists"));

            await _userRepository.AddAsync(user);
            await _encryptionHelper.DecryptAsync(_encryptor, user);
            return user;
        }
    }
}
