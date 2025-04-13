using Grpc.Core;

namespace AuthService.WebApi.Abstractions
{
    public interface IGrpcExceptionMapper
    {
        RpcException MapException(string serviceName, string operation, Exception ex);
    }
}
