using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;
using PasswordService.Domain.Exceptions;

namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public class DeleteCommandHandler
    {
        private readonly IEventSourcingRepository<PasswordAggregate> _writeRepository;

        public DeleteCommandHandler(IEventSourcingRepository<PasswordAggregate> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Guid> HandleAsync(DeletePasswordCommand command, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            PasswordAggregate password = await _writeRepository.GetByIdAsync(command.PasswordId, cancellation)
                ?? throw new PasswordNotFoundException(command.PasswordId);

            PasswordDeleted deletedEvent = new(command.PasswordId);
            password.Apply(deletedEvent);

            await _writeRepository.SaveAsync(password, cancellation);    

            return password.Id;
        }
    }
}
