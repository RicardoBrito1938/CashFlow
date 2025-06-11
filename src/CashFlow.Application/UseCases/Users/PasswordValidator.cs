using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;

public class PasswordValidator<T>: PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        throw new NotImplementedException();
    }

    public override string Name { get; } = "PasswordValidator";
}