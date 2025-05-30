using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase: IRegisterExpenseUseCase
{
    private readonly IExpensesRepositories _repository;

    public RegisterExpenseUseCase(IExpensesRepositories repository)
    {
        _repository = repository;
    }
    public ResponseRegisteredExpenseJson Execute(RequestExpenseJson request)
    {
        Validate(request);
        var entity = new Expense()
        {
            Title = request.Title,
            Description = request.Description,
            Date = request.Date,
            Amount = request.Amount,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };
        
        _repository.Add(entity);
   
        return new ResponseRegisteredExpenseJson();
    }
    
    private void Validate(RequestExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result =  validator.Validate(request);
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);

    }
}