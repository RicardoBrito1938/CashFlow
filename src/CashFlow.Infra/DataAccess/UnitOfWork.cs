using CashFlow.Domain.Repositories;

namespace CashFlow.Infra.DataAccess;

internal class UnitOfWork(CashFlowDbContext dbContext) : IUnitOfWork
{
    public void Commit() => dbContext.SaveChanges();
}