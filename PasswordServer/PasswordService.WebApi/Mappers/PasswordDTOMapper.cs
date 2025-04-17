using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using PasswordService.Application.DTO;
using GrpcPasswordService;
using PasswordService.WebApi.Abstractions;

namespace PasswordService.WebApi.Mappers
{
    public class PasswordDTOMapper : IPasswordDTOMapper
    {
        public PasswordResponse ToPasswordResponse(PasswordDTO passwordDTO)
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
