using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infra.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infra;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
      services.AddScoped<IExpensesRepositories, ExpensesRepository>();
    }
}