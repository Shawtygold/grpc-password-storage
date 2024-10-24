using AuthorisationService.Interfaces.Services;
using AuthorisationService.Model.Entities;
using FluentValidation;
using Grpc.Core;
using RpcExceptionHandlersLib;

namespace AuthorisationService.Services
{
    public class AuthorisationService : AuthorisationProtoService.AuthorisationProtoServiceBase
    {
        private readonly ILogger<AuthorisationService> _logger;
        private readonly IUserAuthenticator _userAuthenticator;
        private readonly IUserRegistration _userRegistration;

        public AuthorisationService(ILogger<AuthorisationService> logger, IUserAuthenticator userAuthenticator, IUserRegistration userRegistration)
        {
            _logger = logger;
            _userAuthenticator = userAuthenticator;
            _userRegistration = userRegistration;
        }

        public override async Task<AuthenticateUserReply> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            AuthenticateUserReply reply = new();

            try
            {
                User user = new(request.Login, request.Password);
                reply.Success = await _userAuthenticator.AuthenticateAsync(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation("Invalid user arguments: " + ex.Message);
                RpcExceptionThrower.Handle(ex);            
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            RegisterUserReply reply = new();

            try
            {
                User user = new(request.Login, request.Passsword);
                reply.Success = await _userRegistration.RegisterAsync(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation("Invalid user arguments: " + ex.Message);
                RpcExceptionThrower.Handle(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)StatusCode.Internal, ex, "Internal Error");
                RpcExceptionThrower.Handle(ex);
            }

            return reply;
        }
    }
}
