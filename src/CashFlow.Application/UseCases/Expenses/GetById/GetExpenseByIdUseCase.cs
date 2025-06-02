using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository repository, IMapper mapper): IGetExpenseByIdUseCase
{
   public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await repository.GetById(id);
        
        return mapper.Map<ResponseExpenseJson>(result);
    }
}