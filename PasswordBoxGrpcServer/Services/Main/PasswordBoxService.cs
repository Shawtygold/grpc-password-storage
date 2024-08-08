using FluentValidation;
using Grpc.Core;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;
using PasswordBoxGrpcServer.Interfaces.Services.Users;
using PasswordBoxGrpcServer.Model.Entities;
using PasswordBoxGrpcServer.Model.Exceptions;

namespace PasswordBoxGrpcServer.Services.Main
{
    public sealed class PasswordBoxService : PasswordBox.PasswordBoxBase
    {
        private readonly ILogger<PasswordBoxService> _logger;
        private readonly IUserService _userService;
        private readonly IPasswordService _passwordService;

        public PasswordBoxService(ILogger<PasswordBoxService> logger, IUserService userService, IPasswordService passwordService)
        {
            _logger = logger;
            _userService = userService;
            _passwordService = passwordService;
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            try
            {
                User user = _userService.CreateUser(request.Login, request.Password);
                await _userService.RegisterAsync(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, "User validation exception");
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

            return new RegisterUserReply { Success = true };
        }

        public override async Task<AuthenticateUserReply> AuthenticateUser(AuthenticateUserRequest request, ServerCallContext context)
        {
            bool result;

            try
            {
                User user = _userService.CreateUser(request.Login, request.Password);
                result = await _userService.AuthenticateAsync(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, "User validation exception");
                throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex.Message, ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

            return new AuthenticateUserReply { Success = result };
        }

        public override async Task<CreatePasswordReply> CreatePassword(CreatePasswordRequest request, ServerCallContext context)
        {
            try
            {
                // Создание и валидация объекта
                Password password = _passwordService.CreatePassword(request.UserLogin, request.Title, request.Login, request.Password, request.Commentary, request.Image);
                // Добавление в базу данных
                await _passwordService.AddPasswordAsync(password);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, "Password validation exception");
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(AppLogEvents.Exception, ex, ex.Message);
                throw new RpcException(new Status(StatusCode.Internal, ex.Message));
            }

            return new CreatePasswordReply { Success = true };
        }
    }
}
