using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using GrpcPasswordService;
using PasswordService.Application.DTO;

namespace PasswordService.WebApi.Extensions
{
    internal static class PasswordDTOExtension
    {
        internal static PasswordResponse ToPasswordResponse(this PasswordDTO passwordDTO)
        {
            return new PasswordResponse
            {
                Id = passwordDTO.Id.ToString(),
                UserId = passwordDTO.UserId.ToString(),
                Title = passwordDTO.Title,
                Login = passwordDTO.Login,
                EncryptedPassword = ByteString.CopyFrom(passwordDTO.EncryptedPassword),
                IconPath = passwordDTO.IconPath,
                Note = passwordDTO.Note,
                CreatedAt = new Timestamp() { Seconds = passwordDTO.CreatedAt.Second, Nanos = passwordDTO.CreatedAt.Nanosecond },
                UpdatedAt = new Timestamp() { Seconds = passwordDTO.UpdatedAt.Second, Nanos = passwordDTO.UpdatedAt.Nanosecond }
            };
        }
    }
}
