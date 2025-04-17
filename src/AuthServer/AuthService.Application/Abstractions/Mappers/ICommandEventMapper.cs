using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Events;

namespace AuthService.Application.Abstractions.Mappers
{
    public interface ICommandEventMapper
    {
        public UserRegistered Map(RegisterUserCommand message);
    }
}
