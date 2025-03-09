using MediatR;
using PasswordService.Application.Abstractions.Messaging;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Queries.GetPasswordByID
{
    public sealed record class GetPasswordByIDQuery(int PasswordID) : IQuery<Password?>;
}
