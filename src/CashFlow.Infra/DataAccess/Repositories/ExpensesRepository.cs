using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infra.DataAccess.Repositories;

internal class ExpensesRepository: IExpensesRepositories
{
    public void Add(Expense expense)
    {
        var dbContext = new CashFlowDbContext();
        dbContext.Expenses.Add(expense);
        dbContext.SaveChanges();
    }
}