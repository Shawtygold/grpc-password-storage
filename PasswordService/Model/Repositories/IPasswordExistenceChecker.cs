using PasswordService.Model.Entities;

namespace PasswordService.Model.Repositories
{
    public interface IPasswordExistenceChecker
    {
        Task<bool> ExistsAsync(int id);
    }
}
