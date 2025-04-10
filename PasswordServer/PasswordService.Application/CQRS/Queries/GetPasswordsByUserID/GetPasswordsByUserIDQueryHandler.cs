using PasswordService.Application.Abstractions.Mappers;
using PasswordService.Application.Abstractions.Repositories;
using PasswordService.Application.DTO;
using PasswordService.Domain.Entities;

namespace PasswordService.Application.CQRS.Queries.GetPasswordsByUserID
{
    public class GetPasswordsByUserIDQueryHandler
    {
        private readonly IProjectionRepository<PasswordView> _readRepository;
        private readonly IPasswordViewMapper _passwordViewMapper;

        public GetPasswordsByUserIDQueryHandler(IProjectionRepository<PasswordView> readRepository, IPasswordViewMapper passwordViewMapper)
        {
            _readRepository = readRepository;
            _passwordViewMapper = passwordViewMapper;
        }

        public async Task<IEnumerable<PasswordDTO>> Handle(GetPasswordsByUserIDQuery command)
        {
            var passwords = await _readRepository.GetAllAsync(command.UserId);
            return passwords.Select(_passwordViewMapper.Map);
        }
    }
}
