using CashFlow.Application.UseCases.Expenses.GetAll;
using CashFlow.Domain.Entities;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using Shouldly;

namespace UseCases.Test.Expenses.GetAll;

public class GetAllExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);
        var useCase = CreateUseCase(loggedUser, expenses);
        var result = await useCase.Execute();

        // Validate response
        result.ShouldNotBeNull();
        result.Expenses.ShouldNotBeNull();
        result.Expenses.Count.ShouldBe(expenses.Count);

        // Validate that each expense is properly mapped
        for (int i = 0; i < expenses.Count; i++)
        {
            var responseExpense = result.Expenses[i];
            var originalExpense = expenses[i];

            responseExpense.Id.ShouldBe(originalExpense.Id);
            responseExpense.Title.ShouldBe(originalExpense.Title);
            responseExpense.Amount.ShouldBe(originalExpense.Amount);
        }
    }
    private GetAllExpensesUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpenseReadOnlyRepositoryBuilder().GetAll(user, expenses).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new GetAllExpensesUseCase(repository, mapper, loggedUser);
    }
}