namespace AuthService.Application.DTO
{
    public record UserDTO(Guid Id, string Login, string Email, string Password, DateTime CreatedAt);
}
