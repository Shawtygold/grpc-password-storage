using GrpcPasswordService;
using PasswordService.Model.CQRS.Commands;
using PasswordService.Model.CQRS.Queries;

namespace PasswordService.Model.Extensions
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
