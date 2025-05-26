using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtils.Requests;
using Shouldly;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterExpenseValidator();
        var request =  RequestExpenseJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void Error_TitleRequired()
    {
        var validator = new RegisterExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.TITLE_REQUIRED);
    }
    
    [Fact]
    public void Error_AmountMustBeGreaterThanZero()
    {
        var validator = new RegisterExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Amount = 0;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
    }
    
    [Fact]
    public void Error_DateMustBeLessOrEqualThanToday()
    {
        var validator = new RegisterExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.DATE_LESS_OR_EQUAL_THAN_TODAY);
    }
    
    [Fact]
    public void Error_PaymentTypeMustBeValid()
    {
        var validator = new RegisterExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentsType)999; // Invalid enum value
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.INVALID_PAYMENT);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Error_InvalidTitle(string invalidTitle)
    {
        var validator = new RegisterExpenseValidator();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = invalidTitle;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors.Count.ShouldBe(1);
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.TITLE_REQUIRED);
    }
}