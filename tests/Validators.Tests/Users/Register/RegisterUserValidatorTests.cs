using CashFlow.Application.UseCases.Users;
using CashFlow.Exception;
using CommonTestUtils.Requests;
using Shouldly;
using Xunit.Abstractions;

namespace Validators.Tests.Users.Register;

public class RegisterUserValidatorTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public RegisterUserValidatorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void Error_Email_Required()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = string.Empty;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.EMAIL_REQUIRED);
    }
    
    [Fact]
    public void Error_Name_Required()
    {
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.NAME_REQUIRED);
    }
    
    [Fact]
    public void Error_Email_Invalid()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = "invalid-email";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.INVALID_EMAIL);
    }
    
    [Fact]
    public void Error_Password_Invalid()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = string.Empty;
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.PASSWORD_INVALID);
    }
    
    [Fact]
    public void Error_Password_Too_Short()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = "short";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.PASSWORD_INVALID);
    }
    
    [Fact]
    public void Error_Password_No_Uppercase()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = "lowercase1!";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.PASSWORD_INVALID);
    }
    
    [Fact]
    public void Error_Password_No_Lowercase()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = "UPPERCASE1!";
        
        //Act
        var result = validator.Validate(request);
        
        //Assert
        result.IsValid.ShouldBeFalse();
        result.Errors[0].ErrorMessage.ShouldBe(ResourceErrorMessages.PASSWORD_INVALID);
    }
}