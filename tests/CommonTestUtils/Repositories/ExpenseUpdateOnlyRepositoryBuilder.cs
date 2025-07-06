using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtils.Repositories;

public class ExpenseUpdateOnlyRepositoryBuilder
{
    private readonly Mock<IExpensesUpdateOnlyRepository> _repository;
    
    public ExpenseUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IExpensesUpdateOnlyRepository>();
    }
    
    public ExpenseUpdateOnlyRepositoryBuilder GetById(User user, Expense? expense = null)
    {
        if(expense is not null) _repository.Setup(_repository => _repository.GetById(user, expense.Id)).ReturnsAsync(expense);
        return this;
    }
    
    public IExpensesUpdateOnlyRepository Build() => _repository.Object;
}