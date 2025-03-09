using MediatR;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;

namespace PasswordService.Application.CQRS.Queries.GetPasswordByID
{
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
