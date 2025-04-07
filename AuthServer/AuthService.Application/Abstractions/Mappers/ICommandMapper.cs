using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Domain.Events;

namespace AuthService.Application.Abstractions.Mappers
{
    public interface ICommandMapper
    {
        public Task<UserRegistered> ToUserRegisteredEvent(RegisterUserCommand message);
    }
}
