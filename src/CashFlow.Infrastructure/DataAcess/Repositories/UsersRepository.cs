using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAcess.Repositories;
internal class UsersRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly CashFlowDbContext _dbContent;

    public UsersRepository(CashFlowDbContext dbContext)
    {
        _dbContent = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContent.Users.AddAsync(user);
    }

    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContent.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContent.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }
}
