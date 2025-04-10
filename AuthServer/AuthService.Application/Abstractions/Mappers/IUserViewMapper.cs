using AuthService.Application.DTO;
using AuthService.Domain.Entities;

namespace AuthService.Application.Abstractions.Mappers
{
    public interface IUserViewMapper
    {
        UserDTO Map(UserView userView);
    }
}
