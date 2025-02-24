using AuthService.Model.Cryptographers;
using AuthService.Model.Entities;
using AuthService.Model.Errors;
using AuthService.Model.Repositories;

namespace AuthService.Services.Implementation
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptor _encryptor;
        private readonly IEncryptionHelper _encryptionHelper;
        private const string DOMAIN = "";

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
                ?? throw AuthRpcExceptions.NotFound("", user);

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
