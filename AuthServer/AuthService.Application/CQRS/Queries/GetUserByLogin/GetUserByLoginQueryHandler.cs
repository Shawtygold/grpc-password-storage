using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.DTO;
using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;

namespace AuthService.Application.CQRS.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryHandler 
    {
        private readonly IProjectionRepository<UserView> _readRepository;
        private readonly IUserViewMapper _userViewMapper;

        public GetUserByLoginQueryHandler(IProjectionRepository<UserView> readRepository, IUserViewMapper userViewMapper)
        {
            _readRepository = readRepository;
            _userViewMapper = userViewMapper;
        }

        public async Task<UserDTO> HandleAsync(GetUserByLoginQuery query, CancellationToken cancellationToken = default)
        {
             UserView userView = await _readRepository.GetUserByAsync(u => u.Login == query.Login, cancellationToken)
                ?? throw new UserNotFoundException(query.Login);

            return _userViewMapper.Map(userView);
        }
    }
}
