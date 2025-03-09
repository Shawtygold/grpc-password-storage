using PasswordService.Application.Abstractions.Messaging;
using PasswordService.Application.CQRS.DTO;

namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public sealed record DeletePasswordCommand(int PasswordId) : ICommand<PasswordResponse>;
}
