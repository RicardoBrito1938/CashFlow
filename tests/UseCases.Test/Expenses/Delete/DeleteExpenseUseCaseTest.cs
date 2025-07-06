using CashFlow.Application.UseCases.Expenses.Delete;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Repositories;
using Shouldly;

namespace UseCases.Test.Expenses.Delete;

public class DeleteExpenseUseCaseTest
{
    [Fact]
    public async Task Success_Delete_Expense()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user);
        var useCase = CreateUseCase(user, expense);
        var act = async () => await useCase.Execute(expense.Id);
        await act.ShouldNotThrowAsync();
    }
    
    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(1000);
        await act.ShouldThrowAsync<NotFoundException>();
    }
    
    private DeleteExpenseByIdUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repositoryWriteOnly = ExpensesWriteOnlyRepositoryBuilder.Build();
        var repository = new ExpenseReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
           
        return new DeleteExpenseByIdUseCase(repositoryWriteOnly, repository, unitOfWork, loggedUser);
    }
}