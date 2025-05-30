using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infra.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesRepositories
{
    public void Add(Expense expense)
    {
        dbContext.Expenses.Add(expense);
    }
}