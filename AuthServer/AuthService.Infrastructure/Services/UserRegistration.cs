using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using AuthService.Application.DTO;
using Wolverine;

namespace AuthService.Infrastructure.Services
{
    public class UserRegistration : IUserRegistration
    {
        private readonly IMessageBus _messageBus;

        public UserRegistration(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task<Guid> RegisterAsync(RegisterUserDTO userDTO)
        {
            RegisterUserCommand command = new(userDTO.Login, userDTO.Email, userDTO.Password);
            return await _messageBus.InvokeAsync<Guid>(command);
        }
    }
}
