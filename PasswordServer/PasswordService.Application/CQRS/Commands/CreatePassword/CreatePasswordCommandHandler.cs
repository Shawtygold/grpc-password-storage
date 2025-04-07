using Marten;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Application.CQRS.Commands.CreatePassword
{
    public sealed class CreatePasswordCommandHandler 
    {
        //private readonly IDocumentStore _documentStore;
        private readonly IEventSourcingRepository<Password> _writeRepository;
        private readonly ICommandEventMapper _commandMapper;

        public CreatePasswordCommandHandler(/*IDocumentStore documentStore,*/IEventSourcingRepository<Password> writeRepository, ICommandEventMapper commandMapper)
        {
            //_documentStore = documentStore;
            _writeRepository = writeRepository;
            _commandMapper = commandMapper;
        }

        public async Task<Guid> HandleAsync(CreatePasswordCommand command)
        {
            PasswordCreated createdEvent = _commandMapper.Map(Guid.NewGuid(), command);
            Password password = new();
            password.Apply(createdEvent);
            
            await _writeRepository.SaveAsync(password);
            //using var session = _documentStore.LightweightSession();
            //session.Events.StartStream<Password>(createdEvent.Id, createdEvent);
            //await session.SaveChangesAsync();

            return password.Id;
        }
    }
}
