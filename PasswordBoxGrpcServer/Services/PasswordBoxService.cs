using Grpc.Core;

namespace PasswordBoxGrpcServer.Services
{
    public class PasswordBoxService : PasswordBox.PasswordBoxBase
    {
        private readonly ILogger<PasswordBoxService> _logger;
        public PasswordBoxService(ILogger<PasswordBoxService> logger)
        {
            _logger = logger;
        }

        public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            return null;
        }
    }
}
