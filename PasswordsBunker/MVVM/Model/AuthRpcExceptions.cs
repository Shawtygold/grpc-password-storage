using Google.Rpc;
using Grpc.Core;

namespace PasswordBoxClient.MVVM.Model
{
    internal class AuthRpcExceptions
    {
        internal static RpcException AlreadyExists(string domain, int userId, int userLogin)
        {
            ErrorInfo errorInfo = new()
            {
                Domain = domain,
                Reason = "PASSWORD_ALREADY_EXISTS",
                Metadata = { { "user_id", $"{userId}" }, { "user_login", $"{userLogin}" } }
            };

            return new Google.Rpc.Status()
            {
                Code = (int)StatusCode.AlreadyExists,
                Message = "Password already exists",
                Details = { Any.Pack(errorInfo) }
            }.ToRpcException();
        }
    }
}
