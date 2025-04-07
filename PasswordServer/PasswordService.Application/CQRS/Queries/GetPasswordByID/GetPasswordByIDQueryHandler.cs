using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.Exceptions;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.CQRS.Queries.GetPasswordByID
{
    public sealed class GetPasswordByIDQueryHandler
    {
        private readonly IProjectionRepository<Password> _readRepository;

        public GetPasswordByIDQueryHandler(IProjectionRepository<Password> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<Password> Handle(GetPasswordByIDQuery request)
        {
            return await _readRepository.GetByIdAsync(request.PasswordId) 
                ?? throw new PasswordNotFoundException(request.PasswordId);         
        }
    }
}
