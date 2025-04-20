namespace PasswordService.Domain.Events
{
    public sealed record PasswordCreated(Guid Id, Guid UserId, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime CreatedAt) : BaseEvent;
    public sealed record PasswordUpdated(Guid Id, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note, DateTime UpdatedAt) : BaseEvent;
    public sealed record PasswordDeleted(Guid Id) : BaseEvent;
}
