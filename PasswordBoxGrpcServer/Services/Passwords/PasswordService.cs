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

        // Создание и валидация пароля
        public Password CreatePassword(string userLogin, string title, string login, string passwordHash, string commentary, string image)
        {
            return _passwordCreator.Create(userLogin, title, login, passwordHash, commentary, image);
        }

        // Добавление пароля в базу данных
        public async Task AddPasswordAsync(Password password)
        {
            // Шифрование данных
            // TODO: Add an encryptor
            byte[] encryptedPassword = await _encryptor.EncryptAsync(password.PasswordValue);
            byte[] encryptedLogin = await _encryptor.EncryptAsync(password.Login);

            // Добавление в базу данных
            await _passwordRepository.AddAsync(password);
        }

        // Обновление пароля
        public async Task UpdatePasswordAsync(Password password)
        {
            // TODO: Add an encryptor

            await _passwordRepository.UpdateAsync(password);
        }

        // Удаление пароля из базы данных
        public async Task DeletePasswordAsync(int entityId)
        {
            await _passwordRepository.DeleteAsync(entityId);
        }

        // Получение всех паролей
        public IEnumerable<Password> GetPasswords()
        {
            // TODO: Add a decryptor

            return _passwordRepository.GetAll();
        }
    }
}
