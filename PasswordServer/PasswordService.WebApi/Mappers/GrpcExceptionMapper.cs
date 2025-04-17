using FluentValidation;
using Grpc.Core;
using PasswordService.Domain.Exceptions;
using PasswordService.WebApi.Abstractions;
using PasswordService.WebApi.Exceptions;

namespace PasswordService.WebApi.Mappers
{
    public class GrpcExceptionMapper : IGrpcExceptionMapper
    {
        public RpcException MapException(Exception ex)
        {
            switch (ex)
            {
                case PasswordNotFoundException notFoundEx: 
                    {
                        return PasswordRpcExceptions.NotFound(notFoundEx.PasswordId.ToString());                   
                    }
                case ValidationException validationEx:
                    {
                        return PasswordRpcExceptions.InvalidArgumets(validationEx.Errors);
                    }
                default:
                    {
                        return PasswordRpcExceptions.InternalError(ex);
                    }
            }
        }
    }
}
