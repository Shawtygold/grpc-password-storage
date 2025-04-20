using AuthService.Application.Abstractions.Providers;
using AuthService.Application.Abstractions.Security;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.CQRS.Queries.GetUserByLogin;
using AuthService.Application.DTO;
using AuthService.Domain.Exceptions;
using Wolverine;

namespace AuthService.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMessageBus _messageBus;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJWTProvider _jwtProvider;

        public LoginService(IMessageBus messageBus, IPasswordHasher passwordHasher, IJWTProvider jwtProvider)
        {
            _messageBus = messageBus;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> LoginAsync(string login, string password, CancellationToken cancellation = default)
        {
            cancellation.ThrowIfCancellationRequested();

            GetUserByLoginQuery query = new(login);
            UserDTO user = await _messageBus.InvokeAsync<UserDTO>(query, cancellation);

            if (!_passwordHasher.Verify(password, user.PasswordHash))
                throw new AuthenticationException();

            return _jwtProvider.GetJWTToken(user.Id);
        }
    }
}
