namespace CashFlow.Domain.Repositories.User;

public interface IUsersReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
}