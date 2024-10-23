using Grpc.Core;
using AuthorisationService.Interfaces.Repositories;
using AuthorisationService.Model.Entities;
using AuthorisationService.Interfaces.Cryptographers;
using AuthorisationService.Interfaces.Services;

namespace AuthorisationService.Services
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
