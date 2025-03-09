using MediatR;
using PasswordService.Application.Abstractions.Messaging;
using PasswordService.Application.CQRS.DTO;

namespace PasswordService.Application.CQRS.Commands.UpdatePassword
{
    public sealed record UpdatePasswordCommand(
        int ID,
        int UserID,
        string Title,
        string Login,
        string Password,
        string IconPath,
        string Note,
        DateTime CreatedAt,
        DateTime UpdatedAt) : ICommand<PasswordResponse>;
}
