using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories;

public class UsersRepository(CashFlowDbContext dbContext) : IUsersReadOnlyRepository, IUsersWriteOnlyRepository
{
    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
       return await dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public async Task Add(User user)
    {
       await dbContext.Users.AddAsync(user);
    }
}