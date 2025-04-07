namespace AuthService.Domain.Events
{
    public sealed record UserRegistered(Guid Id, string Login, string Email, byte[] EncryptedPassword, DateTime CreatedAt) : BaseEvent(Id);
}
