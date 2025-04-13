using FluentValidation;
using Grpc.Core;
using PasswordService.Domain.Exceptions;
using PasswordService.WebApi.Abstractions;
using PasswordService.WebApi.Exceptions;

namespace PasswordService.WebApi.Mappers
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
                case PasswordNotFoundException notFoundEx: 
                    {

                        _logger.LogWarning("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, domain, operation, "Failed", notFoundEx.Message);
                        return PasswordRpcExceptions.NotFound(domain, notFoundEx.PasswordId.ToString());                   
                    }
                case ValidationException validationEx:
                    {
                        _logger.LogWarning("{Date} {Domain} {Operation} {Status} {Message}", DateTime.Now, domain, operation, "Validation Failed", ex.Message);
                        return PasswordRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        _logger.LogError("{Date} {Domain} {Operation} {Status} {ExMessage}", DateTime.Now, domain, operation, "Failed", ex.Message);
                        return PasswordRpcExceptions.InternalError(domain, ex);
                    }
            }
        }
    }
}
