using AuthorisationService.Interfaces.Cryptographers;
using AuthorisationService.Interfaces.Repositories;
using AuthorisationService.Interfaces.Services;
using AuthorisationService.Model.Entities;

namespace AuthorisationService.Services
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

            User? dbUser = await _userRepository.GetByAsync(u => u.Login == user.Login && u.Password == user.Password);

            if (dbUser == null)
                return false;

            return true;
        }
    }
}
