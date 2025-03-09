using MediatR;
using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Messaging;
using PasswordService.Application.CQRS.DTO;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Queries.GetPasswordsByUserID
{
    public record GetPasswordsByUserIDQuery(int UserID) : IQuery<IEnumerable<PasswordResponse>>;
}
