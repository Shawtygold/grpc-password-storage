using GrpcPasswordService;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;

namespace PasswordService.WebApi.Extensions
{
    internal static class RequestExtension
    {
        internal static CreatePasswordCommand ToCommand(this CreatePasswordRequest request, Guid userId)
        {
            return new CreatePasswordCommand(
                userId,
                request.Title,
                request.Login,
                request.EncryptedPassword.ToArray(),
                request.IconPath,
                request.Note);
        }

        internal static UpdatePasswordCommand ToCommand(this UpdatePasswordRequest request, Guid userId)
        {
            return new UpdatePasswordCommand(
                Guid.Parse(request.Password.Id),
                userId,
                request.Password.Title,
                request.Password.Login,
                request.Password.EncryptedPassword.ToArray(),
                request.Password.IconPath,
                request.Password.Note,
                request.Password.CreatedAt.ToDateTime(),
                request.Password.UpdatedAt.ToDateTime());
        }

        internal static DeletePasswordCommand ToCommand(this DeletePasswordRequest request)
        {
            return new DeletePasswordCommand(Guid.Parse(request.Id));
        }
    }
}
