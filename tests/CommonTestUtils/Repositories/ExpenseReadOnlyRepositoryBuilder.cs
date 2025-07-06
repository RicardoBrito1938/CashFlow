using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtils.Repositories;

public class ExpenseReadOnlyRepositoryBuilder
{
  private readonly Mock<IExpensesReadOnlyRepository> _repository = new();

  public ExpenseReadOnlyRepositoryBuilder GetAll(User user, List<Expense> expenses)
  {
    _repository.Setup(expenseReadOnlyRepository => expenseReadOnlyRepository.GetAll(user)).ReturnsAsync(expenses);
    return this;
  }
  
  
public ExpenseReadOnlyRepositoryBuilder GetById(User user, Expense? expense)
  {
    
    if(expense is null) return this;

    _repository.Setup(expenseReadOnlyRepository => expenseReadOnlyRepository.GetById(user, expense.Id))
      .ReturnsAsync(expense);
    return this;
  }

public ExpenseReadOnlyRepositoryBuilder FilterByMonth(User user, List<Expense> expenses)
 {
   _repository.Setup(expenseReadOnlyRepository => expenseReadOnlyRepository.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);
    return this;
 }

public IExpensesReadOnlyRepository Build()
  {
    return _repository.Object;
  }
}