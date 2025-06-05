using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Fonts;

public class ExpensesReportFontsResolver: IFontResolver
{
    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    public byte[]? GetFont(string faceName)
    {
        throw new NotImplementedException();
    }
}