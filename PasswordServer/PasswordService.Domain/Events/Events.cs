namespace PasswordService.Domain.Events
{
    public sealed record PasswordCreated(Guid Id, Guid UserId, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime CreatedAt);
    public sealed record PasswordUpdated(Guid Id, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime UpdatedAt);
    public sealed record PasswordDeleted(Guid Id);
}
