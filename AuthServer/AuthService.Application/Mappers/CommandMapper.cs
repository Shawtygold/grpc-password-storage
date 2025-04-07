using AuthService.Application.Abstractions.Encryption;
using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Events;

namespace AuthService.Application.Mappers
{
    public class CommandMapper : ICommandMapper
    {
        private readonly IEncryptor _encryptor;

        public CommandMapper(IEncryptor encryptor)
        {
            _encryptor = encryptor;
        }

        public async Task<UserRegistered> ToUserRegisteredEvent(RegisterUserCommand command)
        {
            return new UserRegistered(
                Guid.NewGuid(),
                command.Login,
                command.Email,
                await _encryptor.EncryptAsync(command.Password),
                DateTime.Now);
        }
    }
}
