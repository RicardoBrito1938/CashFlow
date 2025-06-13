using CashFlow.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories;

public class UserRepository(CashFlowDbContext dbContext) : IUsersReadOnlyRepository
{
    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
       return await dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
}