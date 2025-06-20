using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtils.Repositories;

public class UsersReadOnlyRepositoryBuilder
{
    private readonly Mock<IUsersReadOnlyRepository> _repository = new();

    public void ExistsActiveUserWithEmail(string email)
    {
        _repository.Setup(userReadOnlyRepository => userReadOnlyRepository.ExistsActiveUserWithEmail(email)).ReturnsAsync(true);
    }
    
    public UsersReadOnlyRepositoryBuilder GetByEmail(User user)
    {
        _repository.Setup(userReadOnlyRepository => userReadOnlyRepository.GetByEmail(user.Email))
            .ReturnsAsync(user);
        
        return this;
    }
    
    public IUsersReadOnlyRepository Build() => _repository.Object;
}