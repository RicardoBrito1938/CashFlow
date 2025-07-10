using CashFlow.Application.UseCases.Users.ChangePassword;
using CommonTestUtils.Requests;
using Shouldly;

namespace Validators.Tests.ChangePassword;

public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdatePasswordValidator();
        var request =  RequestUpdateUserPasswordJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }
}