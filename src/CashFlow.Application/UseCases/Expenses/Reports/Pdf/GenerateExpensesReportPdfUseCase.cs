using System.Reflection;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "£";
    private readonly IExpensesReadOnlyRepository _repository;
    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
    {
        _repository = repository;
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _repository.FilterByMonth(month);
        if (expenses.Count == 0) return [];
        
        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePicture(page);
        var totalExpenses = expenses.Sum(expense => expense.Amount);
        CreateTotalExpensesSection(page, month, totalExpenses);

        foreach (var expense in expenses)
        {
            var table = CreateExpenseTable(page);
        }
        
        return RenderDocumentToPdf(document);
    }
    
    private Document CreateDocument(DateOnly month)
    {
        var document = new Document
        {
            Info =
            {
                Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} - {month.ToString("Y")}",
                Author = "Ricardo Brito"
            }
        };

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        
        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;
        
        return section;
    }
    
    private byte[] RenderDocumentToPdf(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };
        renderer.RenderDocument();
        
        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream);
        
        return stream.ToArray();
    }
    
    private void CreateHeaderWithProfilePicture(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");
        var row = table.AddRow();
        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var imagePath = Path.Combine(directoryName!, "Assets", "me.jpeg");
        row.Cells[0].AddImage(imagePath);
        row.Cells[1].AddParagraph("Hey, Ricardo Brito here!");
        row.Cells[1].Format.Font.Name = FontHelper.RALEWAY_BLACK;
        row.Cells[1].Format.Font.Size = 16;
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }
    
    private void CreateTotalExpensesSection(Section page, DateOnly month, decimal totalExpenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";
        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));
        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();
        paragraph.AddFormattedText($"{totalExpenses} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();
        table.Borders.Width = 0.75;
        table.Borders.Color = Colors.Black;
        
        // Define columns
        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        
        return table;
        
    }
}