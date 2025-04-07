using Marten;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Application.CQRS.Commands.UpdatePassword
{
    public sealed class UpdatePasswordCommandHandler 
    {
        //private readonly IDocumentStore _documentStore;
        private readonly IEventSourcingRepository<Password> _writeRepository;
        private readonly ICommandEventMapper _commandMapper;

        public UpdatePasswordCommandHandler(/*IDocumentStore documentStore,*/IEventSourcingRepository<Password> writeRepository, ICommandEventMapper commandMapper)
        {
            //_documentStore = documentStore;
            _writeRepository = writeRepository;
            _commandMapper = commandMapper;
        }

        public async Task<Guid> Handle(UpdatePasswordCommand command)
        {
            Password password = await _writeRepository.GetByIdAsync(command.Id)
                ?? throw new PasswordNotFoundException(command.Id);

            PasswordUpdated updatedEvent = _commandMapper.Map(command);
            password.Apply(updatedEvent);

            await _writeRepository.SaveAsync(password);

            //using var session = _documentStore.LightweightSession();
            //session.Events.Append(updated.Id, updated);
            //await session.SaveChangesAsync();

            return password.Id;
        }
    }
}
