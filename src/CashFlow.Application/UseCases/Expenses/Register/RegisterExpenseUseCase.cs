using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase(
    IExpensesWriteOnlyRepository repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser) : IRegisterExpenseUseCase
{

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpenseJson request)
    {
        Validate(request);
        var loggedUserEntity = await loggedUser.Get();
        var entity = mapper.Map<Expense>(request);
        entity.UserId = loggedUserEntity.Id;
        
        await repository.Add(entity);
        await unitOfWork.Commit();

        return mapper.Map<ResponseRegisteredExpenseJson>(entity);
    }
    
    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var result =  validator.Validate(request);
        if (result.IsValid) return;
        
        var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorMessages);

    }
}