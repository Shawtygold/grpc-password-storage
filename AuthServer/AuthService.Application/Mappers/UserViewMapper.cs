using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.DTO;
using AuthService.Domain.Entities;

namespace AuthService.Application.Mappers
{
    public class UserViewMapper : IUserViewMapper
    {
        public UserDTO Map(UserView userView)
        {
            return new UserDTO(
                userView.Id,
                userView.Login,
                userView.Email,
                userView.PasswordHash,             
                userView.CreatedAt);
        }
    }
}
