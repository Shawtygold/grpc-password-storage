using AuthService.Application.DTO;

namespace AuthService.Application.Abstractions.Services
{
    public interface IUserRegistration
    {
        Task<Guid> RegisterAsync(RegisterUserDTO userDTO);
    }
}
