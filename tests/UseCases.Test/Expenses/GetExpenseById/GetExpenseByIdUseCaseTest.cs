using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using Shouldly;

namespace UseCases.Test.Expenses.GetExpenseById;

public class GetExpenseByIdUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user);
        var useCase = CreateUseCase(user, expense);

        // Act
        var result = await useCase.Execute(expense.Id);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(expense.Id);
        result.Title.ShouldBe(expense.Title);
        result.Amount.ShouldBe(expense.Amount);
    }
    
    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(id: 1000);
        var result =  await act.ShouldThrowAsync<NotFoundException>();
    }
    
    private GetExpenseByIdUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpenseReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new GetExpenseByIdUseCase(repository, mapper, loggedUser);
    }
}