using System.Text.RegularExpressions;
using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;

namespace CashFlow.Application.UseCases.Users;

public partial class PasswordValidator<T>: PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE = "ErrorMessage";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE}}}";
    }
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 8 || UppercaseLetters().IsMatch(password) == false || LowerCaseLetters().IsMatch(password) == false || NumbersRegex().IsMatch(password) == false || MyRegex().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.PASSWORD_INVALID);
            return false;
        }


        return true;
    }

    public override string Name { get; } = "PasswordValidator";

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UppercaseLetters();
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseLetters();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex NumbersRegex();
    [GeneratedRegex(@"[\W_]+")]
    private static partial Regex MyRegex();
}