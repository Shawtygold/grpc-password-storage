namespace PasswordService.Application.CQRS.Commands.UpdatePassword
{
    public sealed record UpdatePasswordCommand(
        Guid Id,
        Guid UserId,
        string Title,
        string Login,
        byte[] EncryptedPassword,
        string IconPath,
        string Note,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}
