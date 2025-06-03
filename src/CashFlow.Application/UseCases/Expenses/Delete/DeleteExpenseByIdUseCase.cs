using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete;

public class DeleteExpenseByIdUseCase(IExpensesWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteExpenseByIdUseCase
{
    public async Task Execute(long id)
    {
       var response =  await repository.Delete(id);
       
        if (!response)
        {
            throw new NotFoundException(ResourceErrorMessages.NOT_FOUND);
        }
        
        await unitOfWork.Commit();
    }
}
