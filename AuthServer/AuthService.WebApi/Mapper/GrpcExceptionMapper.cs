using AuthService.Domain.Exceptions;
using AuthService.WebApi.Abstractions;
using AuthService.WebApi.Exceptions;
using FluentValidation;
using Grpc.Core;

namespace AuthService.WebApi.Mapper
{
    public class GrpcExceptionMapper : IGrpcExceptionMapper
    {
        private readonly ILogger<GrpcExceptionMapper> _logger;

        public GrpcExceptionMapper(ILogger<GrpcExceptionMapper> logger)
        {
            _logger = logger;
        }

        public RpcException MapException(Exception ex)
        {
            switch (ex)
            {
                case UserNotFoundException notFoundEx: 
                    {
                        _logger.LogWarning("{Date} User not found. UserId: {Id}", DateTime.Now, notFoundEx.UserLogin);
                        return AuthRpcExceptions.NotFound(notFoundEx.UserLogin);                   
                    }
                case ValidationException validationEx:
                    {
                        _logger.LogWarning("{Date} Validation failed \"{Message}\"", DateTime.Now, ex.Message);
                        return AuthRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        _logger.LogError("{Date} Failed \"{Message}\"", DateTime.Now, ex.Message);
                        return AuthRpcExceptions.InternalError(ex);
                    }
            }
        }
    }
}
