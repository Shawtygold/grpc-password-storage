namespace PasswordService.Application.DTO
{
    public record PasswordDTO(Guid Id, Guid UserId, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime CreatedAt, DateTime UpdatedAt);
}
