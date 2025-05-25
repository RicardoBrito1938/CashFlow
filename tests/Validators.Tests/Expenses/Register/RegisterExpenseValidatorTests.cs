using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterExpenseValidator();
        var request = new RequestExpenseJson()
        {
            Amount = 100,
            Date = DateTime.Now.AddDays(-1), // Date in the past
            Title = "Test Expense",
            Description = "This is a test expense",
            PaymentType = CashFlow.Communication.Enums.PaymentsType.CreditCard
        };
        
        var result = validator.Validate(request);
        
        Assert.True(result.IsValid, "Validation should succeed");
    }
}