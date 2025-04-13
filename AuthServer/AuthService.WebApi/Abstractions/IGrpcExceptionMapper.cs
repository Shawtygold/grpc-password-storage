using Grpc.Core;

namespace AuthService.WebApi.Abstractions
{
    public interface IGrpcExceptionMapper
    {
        RpcException MapException(string domain, string operation, Exception ex);
    }
}
