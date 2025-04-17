using GrpcPasswordService;
using PasswordService.Application.DTO;

namespace PasswordService.WebApi.Abstractions
{
    public interface IPasswordDTOMapper
    {
        PasswordResponse ToPasswordResponse(PasswordDTO passwordDTO);
    }
}
