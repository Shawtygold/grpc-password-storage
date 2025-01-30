using AuthorisationService.Model.Cryptographers;
using AuthorisationService.Model.Entities;
using AuthorisationService.Model.Repositories;
using Grpc.Core;

namespace AuthorisationService.Services.Implementation
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;
        private readonly IEncryptionHelper _encryptionHelper;

        public UserAuthenticator(IUserRepository userRepository, IEncryptor encryptor, IEncryptionHelper encryptionHelper)
        {
            _userRepository = userRepository;
            _encryptor = encryptor;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<User?> AuthenticateAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            // Encryption
            await _encryptionHelper.EncryptAsync(_encryptor, user);

            User? dbUser = await _userRepository.GetByAsync(u => u.Login == user.Login)
                ?? throw new RpcException(new Status(StatusCode.NotFound, "User with this login was not found."));

            User? result = null;
            if (dbUser.Password == user.Password)
            {
                await _encryptionHelper.DecryptAsync(_encryptor, dbUser);
                result = dbUser;
            }

            return result;
        }
    }
}
