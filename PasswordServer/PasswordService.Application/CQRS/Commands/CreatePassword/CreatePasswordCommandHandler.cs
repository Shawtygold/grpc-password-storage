using Marten;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Application.CQRS.Commands.CreatePassword
{
    public sealed class CreatePasswordCommandHandler 
    {
        private readonly IEventSourcingRepository<PasswordAggregate> _writeRepository;
        private readonly ICommandEventMapper _commandMapper;

        public CreatePasswordCommandHandler(IEventSourcingRepository<PasswordAggregate> writeRepository, ICommandEventMapper commandMapper)
        {
            _writeRepository = writeRepository;
            _commandMapper = commandMapper;
        }

        public async Task<Guid> HandleAsync(CreatePasswordCommand command)
        {
            PasswordCreated createdEvent = _commandMapper.Map(Guid.NewGuid(), command);
            PasswordAggregate password = new();
            password.Apply(createdEvent);
            
            await _writeRepository.SaveAsync(password);

            return password.Id;
        }
    }
}
