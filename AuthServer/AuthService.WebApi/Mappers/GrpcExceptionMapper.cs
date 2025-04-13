using AuthService.Domain.Exceptions;
using AuthService.WebApi.Abstractions;
using AuthService.WebApi.Exceptions;
using FluentValidation;
using Grpc.Core;

namespace AuthService.WebApi.Mappers
{
    public class GrpcExceptionMapper : IGrpcExceptionMapper
    {
        private readonly ILogger<GrpcExceptionMapper> _logger;

        public GrpcExceptionMapper(ILogger<GrpcExceptionMapper> logger)
        {
            _logger = logger;
        }

        public RpcException MapException(string domain, string operation, Exception ex)
        {
            switch (ex)
            {
                case UserNotFoundException notFoundEx: 
                    {
                        _logger.LogWarning("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, domain, operation, "Failed", notFoundEx.Message);
                        return AuthRpcExceptions.NotFound(domain, notFoundEx.UserLogin);                   
                    }
                case ValidationException validationEx:
                    {
                        _logger.LogWarning("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, domain, operation, "Validation Failed", ex.Message);
                        return AuthRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        _logger.LogError("{Date} {Domain} {Operation} {Status} {ExMessage}", DateTime.Now, domain, operation, "Failed", ex.Message);
                        return AuthRpcExceptions.InternalError(domain, ex);
                    }
            }
        }
    }
}
