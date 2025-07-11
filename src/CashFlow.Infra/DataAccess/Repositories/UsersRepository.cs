using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories;

public class UsersRepository(CashFlowDbContext dbContext) : IUsersReadOnlyRepository, IUsersWriteOnlyRepository, IUserUpdateOnlyRepository
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

    public async Task Delete(User user)
    {
        var userToDelete = await dbContext.Users.FirstAsync(u => u.Id == user.Id);
        dbContext.Users.Remove(userToDelete);
    }

    public async Task<User> GetById(long id)
    {
        return await dbContext.Users.FirstAsync(user => user.Id == id);
    }

    public void Update(User user)
    {
        dbContext.Users.Update(user);
    }
}