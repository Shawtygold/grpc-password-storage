using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Commands.RegisterUser;
using Wolverine;

namespace AuthService.Application.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMessageBus _messageBus;

        public RegistrationService(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task<Guid> RegisterAsync(string login, string email, string password, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            RegisterUserCommand command = new(login, email, password);
            Guid userId = await _messageBus.InvokeAsync<Guid>(command, cancellation);

            return userId;
        }
    }
}
