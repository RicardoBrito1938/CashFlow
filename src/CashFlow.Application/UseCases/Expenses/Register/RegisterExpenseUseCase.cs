using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(IExpensesRepositories repository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{

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
        
        repository.Add(entity);
        unitOfWork.Commit();
   
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