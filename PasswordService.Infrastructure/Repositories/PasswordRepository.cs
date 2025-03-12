using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PasswordService.Domain.Entities;
using PasswordService.Domain.Repositories;
using PasswordService.Infrastructure.AppContext;
using System.Linq.Expressions;

namespace PasswordService.Infrastructure.Repositories
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
            entity.UpdatedAt = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Password>> GetCollectionByAsync(Expression<Func<Password, bool>> expression)
        {
            return await Task.Run(() => _dbContext.Passwords.AsNoTracking().Where(expression));
        }

        public async Task<Password?> GetByIDAsync(int entityId)
        {
            Password? entity = await _dbContext.Passwords.FindAsync(entityId);

            if (entity != null)
                _dbContext.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression)
        {
            Password? entity = await _dbContext.Passwords.AsNoTracking().FirstOrDefaultAsync(expression);

            return entity;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
