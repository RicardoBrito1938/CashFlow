using CashFlow.Application.UseCases.Users;
using CashFlow.Communication.Requests;
using FluentValidation;
using Shouldly;

namespace Validators.Tests.Users;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData("short")]
    [InlineData("nouppercase1!")]
    [InlineData("NOLOWERCASE1!")]
    [InlineData("NoNumbers!")]
    [InlineData("NoSpecialChars1")]
    public void InvalidPasswords_ShouldReturnFalse(string password)
    {
        // Arrange
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        
        // Act
        var result = validator.IsValid(new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson()), password);
        
        // Assert
        result.ShouldBeFalse();
    }
}