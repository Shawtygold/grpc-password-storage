using Marten;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public class DeleteCommandHandler
    {
        //private readonly IDocumentStore _documentStore;
        private readonly IEventSourcingRepository<Password> _writeRepository;

        public DeleteCommandHandler(/*IDocumentStore documentStore*/IEventSourcingRepository<Password> writeRepository)
        {
            //_documentStore = documentStore;
            _writeRepository = writeRepository;
        }

        public async Task<Guid> HandleAsync(DeletePasswordCommand command)
        {
            Password password = await _writeRepository.GetByIdAsync(command.PasswordId)
                ?? throw new PasswordNotFoundException(command.PasswordId);

            PasswordDeleted deletedEvent = new(command.PasswordId);
            password.Apply(deletedEvent);

            await _writeRepository.SaveAsync(password);    
            //using var session = _documentStore.LightweightSession();
            //session.Delete<Password>(command.PasswordId);
            //await session.SaveChangesAsync();

            return password.Id;
        }
    }
}
