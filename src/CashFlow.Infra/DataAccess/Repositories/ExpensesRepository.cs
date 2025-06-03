using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository
{
    public async Task Add(Expense expense)
    {
       await dbContext.Expenses.AddAsync(expense);
    }

    //Not a fan of this implementation, but it is the simplest way to implement a delete method in EF Core
    public async Task<bool> Delete(long id)
    {
       var result = await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
         if (result is null)
         {
              return false;
         }
         dbContext.Expenses.Remove(result);
         return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        
       return await dbContext.Expenses.AsNoTracking().ToListAsync();
    }
    
    public async Task<Expense?> GetById(long id)
    {
        return await dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }
}