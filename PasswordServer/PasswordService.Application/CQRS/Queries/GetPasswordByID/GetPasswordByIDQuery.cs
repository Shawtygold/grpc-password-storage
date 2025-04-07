namespace PasswordService.Application.CQRS.Queries.GetPasswordByID
{
    public sealed record class GetPasswordByIDQuery(Guid PasswordId);
}
