using Grpc.Core;

namespace PasswordService.WebApi.Abstractions
{
    public interface IGrpcExceptionMapper
    {
        RpcException MapException(string domain, string operation, Exception ex);
    }
}
