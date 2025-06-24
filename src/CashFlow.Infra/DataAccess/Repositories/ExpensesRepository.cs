using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess.Repositories;

internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
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

    public void Update(Expense expense)
    {
        dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> GetAll()
    {
        
       return await dbContext.Expenses.AsNoTracking().ToListAsync();
    }
    
    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {
        return await dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(date.Year, date.Month,  1).Date;
        var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
        var endDate = new DateTime(date.Year, date.Month, daysInMonth, 23, 59, 59);

        return await dbContext
            .Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(expense => expense.Date)
            .ToListAsync();
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(User user, long id)
    {
        return await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
    }
}