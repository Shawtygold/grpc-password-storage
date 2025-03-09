using PasswordService.Application.Abstractions.Encryption;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.Mappers
{
    public class PasswordMapper : IPasswordMapper
    {
        private readonly IEncryptor _encryptor;

        public PasswordMapper(IEncryptor encryptor)
        {
            _encryptor = encryptor;
        }

        public async Task<Password> ToPassword(CreatePasswordCommand command)
        {
            return new Password(
                command.UserID,
                command.Title,
                command.Login,
                await _encryptor.EncryptAsync(command.Password),
                command.IconPath,
                command.Note,
                DateTime.Now,
                DateTime.Now);
        }
        public async Task<Password> ToPassword(UpdatePasswordCommand command)
        {
            return new Password(
                command.ID,
                command.UserID,
                command.Title,
                command.Login,
                await _encryptor.EncryptAsync(command.Password),
                command.IconPath,
                command.Note,
                DateTime.Now,
                DateTime.Now);
        }

        public async Task<PasswordResponse> ToPasswordResponse(Password password)
        {
            return new PasswordResponse(
                password.Id,
                password.UserId,
                password.Title,
                password.Login,
                await _encryptor.DecryptAsync(password.EncryptedPassword),
                password.IconPath,
                password.Note,
                password.CreatedAt,
                password.UpdatedAt);
        }
    }
}
