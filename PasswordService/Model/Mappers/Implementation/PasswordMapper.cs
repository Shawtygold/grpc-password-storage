using Google.Protobuf.WellKnownTypes;
using GrpcPasswordService;
using PasswordService.Model.CQRS.Commands;
using PasswordService.Model.Encryption;
using PasswordService.Model.Entities;

namespace PasswordService.Model.Mappers.Implementation
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

        public async Task<PasswordModel> ToPasswordModel(Password password)
        {
            return new PasswordModel()
            {
                Id = password.Id,
                Title = password.Title,
                Login = password.Login,
                Password = await _encryptor.DecryptAsync(password.EncryptedPassword),
                IconPath = password.IconPath,
                Note = password.Note,
                CreatedAt = new Timestamp() { Seconds = password.CreatedAt.Second, Nanos = password.CreatedAt.Nanosecond },
                UpdatedAt = new Timestamp() { Seconds = password.UpdatedAt.Second, Nanos = password.UpdatedAt.Nanosecond }
            };
        }
    }
}
