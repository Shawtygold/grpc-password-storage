namespace PasswordService.Application.CQRS.Commands.CreatePassword
{
    public sealed record CreatePasswordCommand(Guid UserId, string Title, string Login, byte[] EncryptedPassword, string IconPath, string Note);
}
