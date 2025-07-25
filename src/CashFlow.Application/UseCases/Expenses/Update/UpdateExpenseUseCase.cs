using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(
    IExpensesUpdateOnlyRepository repository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser): IUpdateExpenseUseCase
{
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);
        var loggedUserEntity = await loggedUser.Get();
        var expense = await repository.GetById(loggedUserEntity,id);
        if (expense is null)
            throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
        expense.Tags.Clear();
        mapper.Map(request, expense);
        repository.Update(expense);
        await unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        var validationResult = validator.Validate(request);
        if (validationResult.IsValid) return;
        var errorMessage = validationResult.Errors.Select(f => f.ErrorMessage).ToList();
        throw new ErrorOnValidationException(errorMessage);
    }
}