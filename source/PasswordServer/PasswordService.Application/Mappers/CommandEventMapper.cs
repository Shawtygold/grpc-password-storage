using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Domain.Events;

namespace PasswordService.Application.Mappers
{
    public class CommandEventMapper : ICommandEventMapper
    {
        public PasswordCreated Map(Guid Id, CreatePasswordCommand command)
        {
            return new PasswordCreated(
                Id,
                command.UserId,
                command.Title,
                command.Login,
                command.EncryptedPassword,
                command.IconPath,
                command.Note,
                DateTime.Now);
        }

        public PasswordUpdated Map(UpdatePasswordCommand command)
        {
            return new PasswordUpdated(
                command.Id,
                command.Title,
                command.Login,
                command.EncryptedPassword,
                command.IconPath,
                command.Note,
                DateTime.Now);
        }
    }
}
