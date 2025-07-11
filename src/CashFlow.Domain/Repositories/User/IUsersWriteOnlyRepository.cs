namespace CashFlow.Domain.Repositories.User;

public interface IUsersWriteOnlyRepository
{
    Task Add(Entities.User user);
    Task Delete(Entities.User user);
}