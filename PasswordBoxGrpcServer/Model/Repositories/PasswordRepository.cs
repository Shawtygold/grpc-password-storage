using Microsoft.EntityFrameworkCore;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Entities;

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

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
