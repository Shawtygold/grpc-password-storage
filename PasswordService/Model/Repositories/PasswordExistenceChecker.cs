using PasswordService.Interfaces.Repositories;

namespace PasswordService.Model.Repositories
{
    public class PasswordExistenceChecker : IPasswordExistenceChecker
    {
        private readonly IPasswordRepository _passwordRepository;
        public PasswordExistenceChecker(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task<bool> ExistsAsync(int Id)
        {
            return await _passwordRepository.GetByIDAsync(Id) != null;
        }
    }
}
