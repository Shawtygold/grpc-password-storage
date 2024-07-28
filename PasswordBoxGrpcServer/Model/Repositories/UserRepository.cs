using Microsoft.EntityFrameworkCore;
using PasswordBoxGrpcServer.Interfaces.Repositories;
using PasswordBoxGrpcServer.Model.AppContext;
using PasswordBoxGrpcServer.Model.Entities;
using System.Linq.Expressions;

namespace PasswordBoxGrpcServer.Model.Repositories
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
            return await _dbContext.Users.FirstOrDefaultAsync(expression);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
