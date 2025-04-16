using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Queries.GetUserByLogin;
using AuthService.Application.DTO;
using AuthService.Domain.Exceptions;
using AuthService.Infrastructure.Abstractions.Providers;
using Wolverine;

namespace AuthService.Infrastructure.Services
{
    public class UserAuthenticator : IUserAuthenticator
    {
        private readonly IJWTProvider _jwtTokenProvider;
        private readonly IMessageBus _messageBus;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticator(IMessageBus messageBus, IJWTProvider jwtTokenProvider, IPasswordHasher passwordHasher)
        {
            _messageBus = messageBus;
            _jwtTokenProvider = jwtTokenProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> AuthenticateAsync(string login, string password, CancellationToken cancellation = default)
        {
            GetUserByLoginQuery query = new(login);

            UserDTO user = await _messageBus.InvokeAsync<UserDTO>(query, cancellation);

            if(!_passwordHasher.Verify(password, user.PasswordHash))
                throw new AuthenticationException();

            return _jwtTokenProvider.GetJWTToken(user.Id);
        }
    }
}
