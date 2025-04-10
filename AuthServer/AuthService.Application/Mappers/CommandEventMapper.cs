using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Events;

namespace AuthService.Application.Mappers
{
    public class CommandEventMapper : ICommandEventMapper
    {
        private readonly IPasswordHasher _passwordHasher;

        public CommandEventMapper(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public UserRegistered Map(RegisterUserCommand command)
        {
            return new UserRegistered(
                Guid.NewGuid(),
                command.Login,
                command.Email,
                _passwordHasher.Hash(command.Password),
                DateTime.Now);
        }
    }
}
