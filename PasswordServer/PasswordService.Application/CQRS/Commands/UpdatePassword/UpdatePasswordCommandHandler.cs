using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;
using PasswordService.Domain.Exceptions;

namespace PasswordService.Application.CQRS.Commands.UpdatePassword
{
    public sealed class UpdatePasswordCommandHandler 
    {
        private readonly IEventSourcingRepository<PasswordAggregate> _writeRepository;
        private readonly ICommandEventMapper _commandMapper;

        public UpdatePasswordCommandHandler(IEventSourcingRepository<PasswordAggregate> writeRepository, ICommandEventMapper commandMapper)
        {
            _writeRepository = writeRepository;
            _commandMapper = commandMapper;
        }

        public async Task<Guid> Handle(UpdatePasswordCommand command, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            PasswordAggregate password = await _writeRepository.GetByIdAsync(command.Id, cancellation)
                ?? throw new PasswordNotFoundException(command.Id);

            PasswordUpdated updatedEvent = _commandMapper.Map(command);
            password.Apply(updatedEvent);

            await _writeRepository.SaveAsync(password, cancellation);

            return password.Id;
        }
    }
}
