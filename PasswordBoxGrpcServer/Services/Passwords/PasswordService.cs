using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IEncryptor _encryptor;

        public PasswordService(
            IPasswordRepository passwordRepository,
            IEncryptor encryptor)
        {
            _passwordRepository = passwordRepository;
            _encryptor = encryptor;
        }

        public async Task<Password> AddPasswordAsync(Password password)
        {
            await EncryptPasswordAsync(password);
            await _passwordRepository.AddAsync(password);
            await DecryptPasswordAsync(password);

            return password;
        }

        public async Task<Password> UpdatePasswordAsync(Password password)
        {       
            await EncryptPasswordAsync(password);
            await _passwordRepository.UpdateAsync(password);
            await DecryptPasswordAsync(password);

            return password;
        }

        public async Task DeletePasswordAsync(int passwordId)
        {
            await _passwordRepository.DeleteAsync(passwordId);
        }

        public async Task<IEnumerable<Password>> GetPasswordsByUserLoginAsync(string userLogin)
        {
            string encryptedUserLogin = await _encryptor.EncryptAsync(userLogin);

            List<Password> passwords = new(await _passwordRepository.GetCollectionBy(p => p.UserLogin == encryptedUserLogin));
            Task[] tasks = new Task[passwords.Count];

            for (int i = 0; i < passwords.Count; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => DecryptPasswordAsync(passwords[i]));
            }

            await Task.WhenAll(tasks);
            return passwords;
        }

        private async Task EncryptPasswordAsync(Password password)
        {
            password.Title = await _encryptor.EncryptAsync(password.Title);
            password.Login = await _encryptor.EncryptAsync(password.Login);
            password.PasswordValue = await _encryptor.EncryptAsync(password.PasswordValue);
            password.Image = await _encryptor.EncryptAsync(password.Image);
            password.Commentary = await _encryptor.EncryptAsync(password.Commentary);
        }
        private async Task DecryptPasswordAsync(Password password)
        {
            password.Title = await _encryptor.DecryptAsync(password.Title);
            password.Login = await _encryptor.DecryptAsync(password.Login);
            password.PasswordValue = await _encryptor.DecryptAsync(password.PasswordValue);
            password.Image = await _encryptor.DecryptAsync(password.Image);
            password.Commentary = await _encryptor.DecryptAsync(password.Commentary);
        }
    }
}
