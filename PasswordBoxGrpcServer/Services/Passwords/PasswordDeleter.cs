using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Interfaces.Services.Passwords;

namespace PasswordBoxGrpcServer.Services.Passwords
{
    public class PasswordDeleter : IPasswordDeleter
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordDeleter(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task DeletePasswordAsync(int entityId)
        {
            await _passwordRepository.DeleteAsync(entityId);
        }
    }
}
