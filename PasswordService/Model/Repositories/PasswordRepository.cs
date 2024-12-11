using Microsoft.EntityFrameworkCore;
using PasswordService.Interfaces.Repositories;
using PasswordService.Model.AppContext;
using PasswordService.Model.Entities;
using System.Linq.Expressions;

namespace PasswordService.Model.Repositories
{
    public sealed class PasswordRepository : IPasswordRepository
    {
        private readonly ApplicationContext _dbContext;

        public PasswordRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Password entity)
        {
            await _dbContext.Passwords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int entityId)
        {
            await _dbContext.Passwords.Where(p => p.Id == entityId).ExecuteDeleteAsync();
        }

        public async Task UpdateAsync(Password entity)
        {
            _dbContext.Passwords.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Password>> GetCollectionByAsync(Expression<Func<Password, bool>> expression)
        {
            return await Task.Run(() => _dbContext.Passwords.AsNoTracking().Where(expression));
        }

        public async Task<Password?> GetByIDAsync(int entityId)
        {
            return await _dbContext.Passwords.FindAsync(entityId);
        }

        public async Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression)
        {
            return await _dbContext.Passwords.FirstOrDefaultAsync(expression);         
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
