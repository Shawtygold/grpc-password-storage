using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public class DeleteCommandHandler
    {
        private readonly IEventSourcingRepository<PasswordAggregate> _writeRepository;

        public DeleteCommandHandler(IEventSourcingRepository<PasswordAggregate> writeRepository)
        {
            _writeRepository = writeRepository;
        }

        public async Task<Guid> HandleAsync(DeletePasswordCommand command)
        {
            PasswordAggregate password = await _writeRepository.GetByIdAsync(command.PasswordId)
                ?? throw new PasswordNotFoundException(command.PasswordId);

            PasswordDeleted deletedEvent = new(command.PasswordId);
            password.Apply(deletedEvent);

            await _writeRepository.SaveAsync(password);    

            return password.Id;
        }
    }
}
