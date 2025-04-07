using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.DTO;
using PasswordService.Application.Extensions;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.CQRS.Queries.GetPasswordsByUserID
{
    public class GetPasswordsByUserIDQueryHandler
    {
        private readonly IProjectionRepository<PasswordView> _readRepository;

        public GetPasswordsByUserIDQueryHandler(IProjectionRepository<PasswordView> readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<PasswordDTO>> Handle(GetPasswordsByUserIDQuery command)
        {
            var passwords = await _readRepository.GetAllAsync(command.UserId);

            return passwords.Select(p => p.ToPasswordDTO());
        }
    }
}
