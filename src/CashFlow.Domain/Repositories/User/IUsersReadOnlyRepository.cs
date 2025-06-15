namespace CashFlow.Domain.Repositories.User;

public interface IUsersReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<Entities.User?> GetByEmail(string email);
}