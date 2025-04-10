using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.DTO;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;

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

        public async Task<UserDTO> HandleAsync(GetUserByLoginQuery request)
        {
             UserView userView = await _readRepository.GetUserByAsync(u => u.Login == request.Login)
                ?? throw new UserNotFoundException(request.Login);

            return _userViewMapper.Map(userView);
        }
    }
}
