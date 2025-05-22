using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required");
        RuleFor(expense => expense.Amount)
            .GreaterThan(0)
            .WithMessage("The amount must be greater than zero");
        RuleFor(expense => expense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("The date must be less than or equal to today");
        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage("The payment type is invalid");
    }
}