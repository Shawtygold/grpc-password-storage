using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;

namespace AuthService.Application.CQRS.Queries.CheckUserExists
{
   public class CheckUserExistsQueryHandler
   {
        private readonly IProjectionRepository<UserView> _readRepository;

        public CheckUserExistsQueryHandler(IProjectionRepository<UserView> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<bool> HandleAsync(CheckUserExistsQuery query, CancellationToken cancellationToken = default)
        {
            return await _readRepository.GetUserByAsync(u => u.Login == query.Login, cancellationToken) != null;   
        }
   }
}
