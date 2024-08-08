using Microsoft.EntityFrameworkCore;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Entities;
using System.Linq.Expressions;

namespace PasswordBoxGrpcServer.Model.Repositories
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

        public IEnumerable<Password> GetAll()
        {
            return _dbContext.Passwords;
        }

        public async Task UpdateAsync(Password entity)
        {
            _dbContext.Passwords.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression)
        {
            return await _dbContext.Passwords.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Password>> GetCollectionBy(Expression<Func<Password, bool>> expression)
        {
            return await Task.Run(() => _dbContext.Passwords.Where(expression));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
