using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GeneratedExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository): IGeneratedExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await repository.FilterByMonth(month);
        
        var workbook = new XLWorkbook();
        workbook.Author = "Ricardo Brito";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";
        
        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        
        InsertHeader(worksheet);

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }
    
    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;
        
        worksheet.Row(1).Style.Font.Bold = true;
        worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
        
        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}