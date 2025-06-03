using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase(IExpensesReadOnlyRepository repository, IMapper mapper) : IGetAllExpensesUseCase
{
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await repository.GetAll();
        
        return new ResponseExpensesJson
        {
            Expenses = mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}