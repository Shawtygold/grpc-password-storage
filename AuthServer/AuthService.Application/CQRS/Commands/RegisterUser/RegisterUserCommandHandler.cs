using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;
using AuthService.Domain.Events;

namespace AuthService.Application.CQRS.Commands.RegisterUser
{
    public class RegisterUserCommandHandler
    {
        private readonly ICommandEventMapper _commandEventMapper;
        private readonly IEventSourcingRepository<UserAggregate> _writeRepository;

        public RegisterUserCommandHandler(IEventSourcingRepository<UserAggregate> writeRepository, ICommandEventMapper commandEventMapper)
        {
            _writeRepository = writeRepository;
            _commandEventMapper = commandEventMapper;
        }

        public async Task<Guid> HandleAsync(RegisterUserCommand command)
        {
            UserRegistered registeredEvent = _commandEventMapper.Map(command);
            UserAggregate user = new();
            user.Apply(registeredEvent);

            await _writeRepository.SaveAsync(user);
            return user.Id;
        }
    }
}
