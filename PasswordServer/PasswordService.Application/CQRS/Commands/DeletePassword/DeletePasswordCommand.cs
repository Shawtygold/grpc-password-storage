namespace PasswordService.Application.CQRS.Commands.DeletePassword
{
    public sealed record DeletePasswordCommand(Guid PasswordId);
}
