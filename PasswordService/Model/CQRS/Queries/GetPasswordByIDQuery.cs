using MediatR;
using PasswordService.Model.Entities;
using PasswordService.Model.Repositories;

namespace PasswordService.Model.CQRS.Queries
{
    public sealed record class GetPasswordByIDQuery(int PasswordID) : IRequest<Password?>;

    public sealed class GetPasswordByIDQueryHandler : IRequestHandler<GetPasswordByIDQuery, Password?>
    {
        private readonly IPasswordRepository _passwordRepository;

        public GetPasswordByIDQueryHandler(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task<Password?> Handle(GetPasswordByIDQuery request, CancellationToken cancellationToken)
        {
            return await _passwordRepository.GetByIDAsync(request.PasswordID);      
        }
    }
}
