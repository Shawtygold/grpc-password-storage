//using AuthService.Application.DTO;
//using MediatR;

namespace AuthService.Application.CQRS.Queries.GetUserByLogin
{
    public record GetUserByLoginQuery(string Login);
}
