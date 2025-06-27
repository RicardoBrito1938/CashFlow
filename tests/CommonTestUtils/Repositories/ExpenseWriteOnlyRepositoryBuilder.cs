using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtils.Repositories;

public class ExpensesWriteOnlyRepositoryBuilder
{
    public static IExpensesWriteOnlyRepository Build()
    {
        var mock = new Mock<IExpensesWriteOnlyRepository>();
        return mock.Object;
    }
}