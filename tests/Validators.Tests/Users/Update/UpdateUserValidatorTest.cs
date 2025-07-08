using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Communication.Requests;
using CommonTestUtils.Requests;

namespace Validators.Tests.Users.Update;

public class UpdateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Fail_Name_Empty()
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(RequestUpdateUserProfileJson.Name));
    }
    
    [Fact]
    public void Fail_Email_Empty()
    {
        var validator = new UpdateUserValidator();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Email = string.Empty;
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(RequestUpdateUserProfileJson.Email));
    }
}