using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PasswordService.Model.AppContext;
using PasswordService.Model.Encryption;
using PasswordService.Model.Entities;
using System.Linq.Expressions;

namespace PasswordService.Model.Repositories.Implementation
{
    public sealed class PasswordRepository : IPasswordRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IEncryptor _encryptor;
        private readonly IValidator<Password> _passwordValidator;

        public PasswordRepository(ApplicationContext dbContext, IEncryptor encryptor, IValidator<Password> passwordValidator)
        {
            _dbContext = dbContext;
            _encryptor = encryptor;
            _passwordValidator = passwordValidator;
        }

        public async Task AddAsync(Password entity)
        {
            _passwordValidator.Validate(entity);

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
