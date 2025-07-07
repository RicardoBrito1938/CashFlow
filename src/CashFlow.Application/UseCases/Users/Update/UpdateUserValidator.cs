using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Update;

public class UpdateUserValidator: AbstractValidator<RequestUpdateUserProfileJson>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_REQUIRED);
        RuleFor(user => user.Email)
            .NotEmpty().WithMessage(ResourceErrorMessages.EMAIL_REQUIRED)
            .EmailAddress().WithMessage(ResourceErrorMessages.INVALID_EMAIL)
            .When((user => string.IsNullOrWhiteSpace(user.Email) == false), ApplyConditionTo.CurrentValidator).WithMessage(ResourceErrorMessages.INVALID_EMAIL);
    }
}