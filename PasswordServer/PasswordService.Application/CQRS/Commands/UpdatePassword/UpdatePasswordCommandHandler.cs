using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Events;

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

        public async Task<Guid> Handle(UpdatePasswordCommand command)
        {
            PasswordAggregate password = await _writeRepository.GetByIdAsync(command.Id)
                ?? throw new PasswordNotFoundException(command.Id);

            PasswordUpdated updatedEvent = _commandMapper.Map(command);
            password.Apply(updatedEvent);

            await _writeRepository.SaveAsync(password);

            return password.Id;
        }
    }
}
