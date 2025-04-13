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

        public RpcException MapException(string serviceName, string operation, Exception ex)
        {
            switch (ex)
            {
                case UserNotFoundException notFoundEx: 
                    {

                        _logger.LogWarning("{Date} {ServiceName} {Operation} {Status} {Message}", DateTime.Now, serviceName, operation, "Failed", notFoundEx.UserLogin);
                        return AuthRpcExceptions.NotFound(notFoundEx.UserLogin);                   
                    }
                case ValidationException validationEx:
                    {
                        _logger.LogWarning("{Date} {ServiceName} {Operation} {Status} {Message}", DateTime.Now, serviceName, operation, "Validation Failed", ex.Message);
                        return AuthRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        _logger.LogError("{Date} {ServiceName} {Operation} {Status} {ExMessage}", DateTime.Now, serviceName, operation, "Failed", ex.Message);
                        return AuthRpcExceptions.InternalError(ex);
                    }
            }
        }
    }
}
