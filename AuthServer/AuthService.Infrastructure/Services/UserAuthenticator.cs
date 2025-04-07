using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Queries.GetUserByLogin;
using AuthService.Application.DTO;
using AuthService.Infrastructure.Abstractions.Providers;
using Wolverine;

namespace AuthService.Infrastructure.Services
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IJWTProvider _jwtTokenProvider;
        private readonly IMessageBus _messageBus;

        public UserAuthenticator(IMessageBus messageBus, IJWTProvider jwtTokenProvider)
        {
            _messageBus = messageBus;
            _jwtTokenProvider = jwtTokenProvider;
        }

        public async Task<string> AuthenticateAsync(string login, string password)
        {
            GetUserByLoginQuery query = new(login);

            UserDTO user = await _messageBus.InvokeAsync<UserDTO>(query);

            if (user.Password != password)
                return string.Empty;

            return _jwtTokenProvider.GetJWTToken(user.Id);
        }
    }
}
