using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infra.DataAccess;

internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost;Database=cashflow_db;Uid=root;Pwd=password;";
        var serverVersion = new MySqlServerVersion(new Version(9, 0, 21));
        optionsBuilder.UseMySql(connectionString, serverVersion);
    }
}