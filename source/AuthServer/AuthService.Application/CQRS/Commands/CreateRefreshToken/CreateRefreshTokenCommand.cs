namespace AuthService.Application.CQRS.Commands.CreateRefreshToken
{
    public sealed record CreateRefreshTokenCommand(string Token, Guid UserId, DateTime ExpirationTime);
}
