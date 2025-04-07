using Google.Protobuf.WellKnownTypes;
using GrpcPasswordService;
using PasswordService.Application.CQRS.DTO;

namespace PasswordService.Presentation.Mappers
{
    public static class PasswordResponseMapper
    {
        public static PasswordModel ToPasswordModel(PasswordResponse response)
        {
            return new PasswordModel
            {
                Id = response.Id,
                UserId = response.UserID,
                Title = response.Title,
                Login = response.Login,
                Password = response.Password,
                IconPath = response.IconPath,
                Note = response.Note,
                CreatedAt = new Timestamp() { Seconds = response.CreatedAt.Second, Nanos = response.CreatedAt.Nanosecond },
                UpdatedAt = new Timestamp() { Seconds = response.UpdatedAt.Second, Nanos = response.UpdatedAt.Nanosecond }
            };
        }

        public static CreatePasswordResponse ToCreatePasswordResponse(PasswordResponse response)
        {
            return new CreatePasswordResponse()
            {
                Password = ToPasswordModel(response)
            };
        }

        public static UpdatePasswordResponse ToUpdatePasswordResponse(PasswordResponse response)
        {
            return new UpdatePasswordResponse()
            {
                Password = ToPasswordModel(response)
            };
        }

        public static DeletePasswordResponse ToDeletePasswordResponse(PasswordResponse response)
        {
            return new DeletePasswordResponse()
            {
                Password = ToPasswordModel(response)
            };
        }
    }
}
