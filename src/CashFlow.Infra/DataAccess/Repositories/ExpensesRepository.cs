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
    public async Task Delete(long id)
    {
       var result = await dbContext.Expenses.FindAsync(id);
        dbContext.Expenses.Remove(result!);
    }

    public void Update(Expense expense)
    {
        dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> GetAll(User user)
    {
       return await dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
    }
    
    async Task<Expense?> IExpensesReadOnlyRepository.GetById( User user,long id)
    {
        return await dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id && expense.UserId == user.Id);
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