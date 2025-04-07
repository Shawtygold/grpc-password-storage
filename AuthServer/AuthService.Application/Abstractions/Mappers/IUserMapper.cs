using AuthService.Application.DTO;
using AuthService.Domain.Entities;

namespace AuthService.Application.Abstractions.Mappers
{
    public interface IUserMapper
    {
        public Task<UserDTO> ToUserDTO(User user);
    }
}
