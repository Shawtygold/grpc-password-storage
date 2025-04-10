namespace AuthService.Domain.Events
{
    public sealed record UserRegistered(Guid Id, string Login, string Email, string PasswordHash, DateTime CreatedAt);
}
