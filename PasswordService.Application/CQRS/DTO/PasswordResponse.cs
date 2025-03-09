namespace PasswordService.Application.CQRS.DTO
{
    public record PasswordResponse(int Id, int UserID, string Title, string Login, string Password, string IconPath, string Note, DateTime CreatedAt, DateTime UpdatedAt);
}
