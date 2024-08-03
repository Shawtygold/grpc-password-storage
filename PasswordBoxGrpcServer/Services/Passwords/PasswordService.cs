using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Model.Entities;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordCreator _passwordCreator;
        private readonly IPasswordAdder _passwordAdder;
        private readonly IPasswordUpdater _passwordUpdater;
        private readonly IPasswordDeleter _passwordDeleter;
        private readonly IPasswordRetriever _passwordRetriever;

        public PasswordService(
            IPasswordCreator passwordCreator,
            IPasswordAdder passwordAdder,
            IPasswordUpdater passwordUpdater,
            IPasswordDeleter passwordDeleter,
            IPasswordRetriever passwordRetriever)
        {
            _passwordCreator = passwordCreator;
            _passwordAdder = passwordAdder;
            _passwordUpdater = passwordUpdater;
            _passwordDeleter = passwordDeleter;
            _passwordRetriever = passwordRetriever;
        }

        // Создание и валидация пароля
        public Password CreatePassword(string userLogin, string title, string login, string passwordHash, string commentary, string image)
        {
            return _passwordCreator.CreatePassword(userLogin, title, login, passwordHash, commentary, image);
        }

        // Добавление пароля в базу данных
        public async Task AddPasswordAsync(Password password)
        {
            await _passwordAdder.AddPasswordAsync(password);
        }

        // Обновление пароля
        public async Task UpdatePasswordAsync(Password password)
        {
            await _passwordUpdater.UpdatePasswordAsync(password);
        }

        // Удаление пароля из базы данных
        public async Task DeletePasswordAsync(int entityId)
        {
            await _passwordDeleter.DeletePasswordAsync(entityId);
        }

        // Получение всех паролей
        public IEnumerable<Password> GetPasswords()
        {
            return _passwordRetriever.GetPasswords();
        }
    }
}
