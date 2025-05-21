using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisteredExpenseJson();
    }
    
    private void Validate(RequestExpenseJson request)
    {
        var titleEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (titleEmpty)
            throw new ArgumentException(
                "The title is required",
                nameof(request.Title)
            );
        if (request.Amount <= 0)
        {
            throw new ArgumentException(
                "The amount must be greater than zero",
                nameof(request.Amount)
            );
        }

        var dateResult = DateTime.Compare(request.Date, DateTime.UtcNow);
        if (dateResult > 0)
        {
            throw new ArgumentException(
                "The date must be less than or equal to today",
                nameof(request.Date)
            );
        }
        var isValidPaymentType = Enum.IsDefined(typeof(PaymentsType), request.PaymentType);
        if (!isValidPaymentType)
        {
            throw new ArgumentException(
                "The payment type is invalid",
                nameof(request.PaymentType)
            );
        }
    }
}