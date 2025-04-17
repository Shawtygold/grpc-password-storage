using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Domain.Events;

namespace PasswordService.Application.Abstractions.Mappers
{
    public interface ICommandEventMapper
    {
        PasswordCreated Map(Guid Id, CreatePasswordCommand command);
        PasswordUpdated Map(UpdatePasswordCommand command);
    }
}
