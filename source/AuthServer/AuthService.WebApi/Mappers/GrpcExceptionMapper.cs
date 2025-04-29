using AuthService.Domain.Exceptions;
using AuthService.WebApi.Abstractions;
using AuthService.WebApi.Exceptions;
using FluentValidation;
using Grpc.Core;

namespace AuthService.WebApi.Mappers
{
    public class GrpcExceptionMapper : IGrpcExceptionMapper
    {
        public RpcException MapException(Exception ex)
        {
            switch (ex)
            {
                case UserNotFoundException notFoundEx:
                    {
                        return AuthRpcExceptions.NotFound(notFoundEx.UserLogin);
                    }
                case RefreshTokenExpiredException:
                    {
                        return AuthRpcExceptions.RefreshTokenExpired();
                    }
                case AuthenticationException authEx:
                    {
                        return new RpcException(new Status(StatusCode.Unauthenticated, $"{authEx.Message}"));
                    }
                case ValidationException validationEx:
                    {
                        return AuthRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        return AuthRpcExceptions.InternalError(ex);
                    }
            }
        }
    }
}
