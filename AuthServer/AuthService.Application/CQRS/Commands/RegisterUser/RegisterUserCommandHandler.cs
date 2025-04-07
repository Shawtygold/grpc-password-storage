using AuthService.Application.Abstractions.Mappers;
using AuthService.Domain.Entities;
using AuthService.Domain.Events;
using Marten;

namespace AuthService.Application.CQRS.Commands.RegisterUser
{
    public class RegisterUserCommandHandler
    {
        private readonly ICommandMapper _commandMapper;
        private readonly IDocumentStore _documentStore;

        public RegisterUserCommandHandler(IDocumentStore documentStore, ICommandMapper commandMapper)
        {
            _documentStore = documentStore;
            _commandMapper = commandMapper;
        }

        public async Task<Guid> HandleAsync(RegisterUserCommand message)
        {
            UserRegistered registered = await _commandMapper.ToUserRegisteredEvent(message);

            using var session = _documentStore.LightweightSession();
            session.Events.StartStream<User>(registered.Id, registered);
            await session.SaveChangesAsync();

            return registered.Id;
        }
    }
}
