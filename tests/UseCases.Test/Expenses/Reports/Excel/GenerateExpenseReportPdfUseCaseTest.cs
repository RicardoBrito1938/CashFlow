using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Entities;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Repositories;
using Shouldly;

namespace UseCases.Test.Expenses.Reports.Excel;

public class GenerateExpenseReportPdfUseCaseTest
{
    [Fact]
    private async Task Success()
    {
        // Arrange
        var user = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection( user);        var useCase = CreateUseCase(user, expenses);

        // Act
        var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        // Assert
        result.ShouldNotBeNull();
    }
    
    private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpenseReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
    }
}