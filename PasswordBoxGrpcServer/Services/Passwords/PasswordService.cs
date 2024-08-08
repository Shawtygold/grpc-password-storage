using PasswordBoxGrpcServer.Interfaces.Cryptographers;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordCreator _passwordCreator;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IEncryptor _encryptor;

        public PasswordService(
            IPasswordCreator passwordCreator,
            IPasswordRepository passwordRepository,
            IEncryptor encryptor)
        {
            _passwordCreator = passwordCreator;
            _passwordRepository = passwordRepository;
            _encryptor = encryptor;
        }

        public Password CreatePassword(string userLogin, string title, string login, string passwordHash, string commentary, string image)
        {
            return _passwordCreator.Create(userLogin, title, login, passwordHash, commentary, image);
        }

        public async Task AddPasswordAsync(Password password)
        {
            await EncryptPasswordAsync(password);
            await _passwordRepository.AddAsync(password);
        }

        public async Task UpdatePasswordAsync(Password password)
        {       
            await EncryptPasswordAsync(password);
            await _passwordRepository.UpdateAsync(password);
        }

        public async Task DeletePasswordAsync(int passwordId)
        {
            await _passwordRepository.DeleteAsync(passwordId);
        }

        public IEnumerable<Password> GetAllPasswords()
        {     
            List<Password> passwords = new(_passwordRepository.GetAll());
            Task[] tasks = new Task[passwords.Count];

            for (int i = 0; i < passwords.Count; i++)
            {
                tasks[i] = Task.Factory.StartNew(async () => await DecryptPasswordAsync(passwords[i]));
            }

            Task.WaitAll(tasks);
            return passwords;
        }

        public async Task<IEnumerable<Password>> GetPasswordsByUserLoginAsync(string userLogin)
        {
            string encryptedUserLogin = await _encryptor.EncryptAsync(userLogin);
            return await _passwordRepository.GetCollectionBy(p => p.UserLogin == encryptedUserLogin);
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
