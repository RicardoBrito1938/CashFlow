using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.ChangePassword;

public class UpdatePasswordValidator: AbstractValidator<RequestUpdateUserPasswordJson>
{
    public UpdatePasswordValidator()
    {
        RuleFor(x => x.Password)
            .SetValidator(new PasswordValidator<RequestUpdateUserPasswordJson>());
    }
}