using GrpcPasswordService;
using PasswordService.Application.CQRS.Commands.CreatePassword;
using PasswordService.Application.CQRS.Commands.DeletePassword;
using PasswordService.Application.CQRS.Commands.UpdatePassword;
using PasswordService.Application.CQRS.Queries.GetPasswordsByUserID;

namespace PasswordService.Presentation.Extensions
{
    public static class RequestExtension
    {
        public static CreatePasswordCommand ToCommand(this CreatePasswordRequest request)
        {
            return new CreatePasswordCommand(
                request.UserId,
                request.Title,
                request.Login,
                request.Password,
                request.IconPath,
                request.Note);
        }

        public static UpdatePasswordCommand ToCommand(this UpdatePasswordRequest request)
        {
            return new UpdatePasswordCommand(
                request.Password.Id,
                request.Password.UserId,
                request.Password.Title,
                request.Password.Login,
                request.Password.Password,
                request.Password.IconPath,
                request.Password.Note,
                request.Password.CreatedAt.ToDateTime(),
                request.Password.UpdatedAt.ToDateTime());
        }

        public static DeletePasswordCommand ToCommand(this DeletePasswordRequest request)
        {
            return new DeletePasswordCommand(request.Id);
        }

        public static GetPasswordsByUserIDQuery ToQuery(this GetPasswordsByUserIdRequest request)
        {
            return new GetPasswordsByUserIDQuery(request.UserId);
        }
    }
}
