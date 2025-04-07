using AuthService.Application.Abstractions.Mappers;
using AuthService.Application.DTO;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using Marten;

namespace AuthService.Application.CQRS.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryHandler 
    {
        private readonly IQuerySession _querySession;
        private readonly IUserMapper _userMapper;

        public GetUserByLoginQueryHandler(IQuerySession querySession, IUserMapper userMapper)
        {
            _querySession = querySession;
            _userMapper = userMapper;
        }

        public async Task<UserDTO> HandleAsync(GetUserByLoginQuery request)
        {
            User user = await _querySession.Query<User>().FirstOrDefaultAsync(u => u.Login == request.Login)
                ?? throw new UserNotFoundException(request.Login);

            return await _userMapper.ToUserDTO(user);
        }
    }
}
