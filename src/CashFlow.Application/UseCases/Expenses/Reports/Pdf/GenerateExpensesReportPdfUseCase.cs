using CashFlow.Domain.Repositories.Expenses;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new ExoticFontsFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0) return [];
        
        return [];
    }
}