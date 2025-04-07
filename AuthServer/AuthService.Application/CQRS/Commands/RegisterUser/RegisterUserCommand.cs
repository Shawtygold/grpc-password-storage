namespace AuthService.Application.CQRS.Commands.RegisterUser
{
    public record RegisterUserCommand(string Login, string Email, string Password);
}
