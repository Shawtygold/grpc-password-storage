using PasswordService.Application.Abstractions.Messaging;
using PasswordService.Application.CQRS.DTO;

namespace PasswordService.Application.CQRS.Commands.CreatePassword
{
    public sealed record CreatePasswordCommand(int UserID, string Title, string Login, string Password, string IconPath, string Note) : ICommand<PasswordResponse>;
}
