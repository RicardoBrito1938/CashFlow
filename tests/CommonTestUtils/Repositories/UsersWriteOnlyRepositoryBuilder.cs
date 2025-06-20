using CashFlow.Domain.Repositories.User;
using Moq;

namespace CommonTestUtils.Repositories;

public class UserWriteOnlyRepositoryBuilder
{
    public static IUsersWriteOnlyRepository Build()
    {
        var mock = new Mock<IUsersWriteOnlyRepository>();

        return mock.Object;
    }
}