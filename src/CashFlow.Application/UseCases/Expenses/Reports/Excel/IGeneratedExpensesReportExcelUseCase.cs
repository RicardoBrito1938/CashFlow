namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public interface IGeneratedExpensesReportExcelUseCase
{ 
    Task<byte []> Execute(DateOnly month);
}