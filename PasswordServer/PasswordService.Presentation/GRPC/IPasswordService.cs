using Grpc.Core;
using GrpcPasswordService;

namespace PasswordService.Presentation.GRPC
{
    public interface IPasswordService
    {
        Task<CreatePasswordResponse> CreatePassword(CreatePasswordRequest request, ServerCallContext context);
        Task<UpdatePasswordResponse> UpdatePassword(UpdatePasswordRequest request, ServerCallContext context);
        Task<DeletePasswordResponse> DeletePassword(DeletePasswordRequest request, ServerCallContext context);
        Task<GetPasswordsResponse> GetPasswordsByUserId(GetPasswordsByUserIdRequest request, ServerCallContext context);
    }
}
