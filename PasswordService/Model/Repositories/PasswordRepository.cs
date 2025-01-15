using Microsoft.EntityFrameworkCore;
using PasswordService.Interfaces.Cryptographers;
using PasswordService.Interfaces.Repositories;
using PasswordService.Model.AppContext;
using PasswordService.Model.Entities;
using System.Linq.Expressions;

namespace PasswordService.Model.Repositories
{
    public sealed class PasswordRepository : IPasswordRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IEncryptor _encryptor;

        public PasswordRepository(ApplicationContext dbContext, IEncryptionHelper encryptionHelper, IEncryptor encryptor)
        {
            _dbContext = dbContext;
            _encryptionHelper = encryptionHelper;
            _encryptor = encryptor;
        }

        public async Task AddAsync(Password entity)
        {
            await _encryptionHelper.EncryptAsync(_encryptor, entity);
            await _dbContext.Passwords.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            await _encryptionHelper.DecryptAsync(_encryptor, entity);
        }

        public async Task DeleteAsync(int entityId)
        {
            await _dbContext.Passwords.Where(p => p.Id == entityId).ExecuteDeleteAsync();
        }

        public async Task UpdateAsync(Password entity)
        {
            await _encryptionHelper.EncryptAsync(_encryptor, entity);
            _dbContext.Passwords.Update(entity);
            await _dbContext.SaveChangesAsync();
            await _encryptionHelper.DecryptAsync(_encryptor, entity);
        }

        public async Task<IEnumerable<Password>> GetCollectionByAsync(Expression<Func<Password, bool>> expression)
        {
            return await Task.Run(() => _dbContext.Passwords.AsNoTracking().Where(expression));
        }

        public async Task<Password?> GetByIDAsync(int entityId)
        {
            Password? entity = await _dbContext.Passwords.FindAsync(entityId);
            if(entity != null)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
                await _encryptionHelper.DecryptAsync(_encryptor, entity);
            }
            return entity;
        }

        public async Task<Password?> GetByAsync(Expression<Func<Password, bool>> expression)
        {
            Password? entity = await _dbContext.Passwords.AsNoTracking().FirstOrDefaultAsync(expression);    
            if(entity != null)
                await _encryptionHelper.DecryptAsync(_encryptor, entity);

            return entity;
        }
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
