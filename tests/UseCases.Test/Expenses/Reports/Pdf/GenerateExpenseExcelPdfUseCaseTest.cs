using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Domain.Entities;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Repositories;
using Shouldly;

namespace UseCases.Test.Expenses.Reports.Pdf;

public class GenerateExpenseExcelPdfUseCaseTest
{
    [Fact]
    private void Success()
    {
        // Arrange
        var user = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection( user);
        var useCase = CreateUseCase(user, expenses);

        // Act
        var result = useCase.Execute(DateOnly.FromDateTime(DateTime.Today));

        // Assert
        result.ShouldNotBeNull();
    }
    
    private GeneratedExpensesReportExcelUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpenseReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new GeneratedExpensesReportExcelUseCase(repository, loggedUser);
    }
}