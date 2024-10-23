using AuthorisationService.Interfaces.Repositories;
using AuthorisationService.Model.AppContext;
using AuthorisationService.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthorisationService.Model.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByAsync(Expression<Func<User, bool>> expression)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
