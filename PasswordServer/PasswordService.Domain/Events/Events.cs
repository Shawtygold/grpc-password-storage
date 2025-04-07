namespace PasswordService.Domain.Events
{
    public record PasswordCreated(Guid Id, Guid UserId, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime CreatedAt);
    public record PasswordUpdated(Guid Id, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime UpdatedAt);
    public record PasswordDeleted(Guid Id);
}
