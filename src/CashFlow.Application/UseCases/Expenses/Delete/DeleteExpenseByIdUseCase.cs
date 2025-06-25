using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseByIdUseCase(IExpensesWriteOnlyRepository repository, IExpensesReadOnlyRepository readOnlyRepository, IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IDeleteExpenseByIdUseCase
{
    public async Task Execute(long id)
    {
        var user = await loggedUser.Get();
        var expense = await readOnlyRepository.GetById(user, id);
        if (expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
        }
        await repository.Delete(id);
        await unitOfWork.Commit();
    }
}
