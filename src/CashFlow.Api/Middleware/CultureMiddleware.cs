using System.Globalization;

namespace CashFlow.Api.Middleware;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task Invoke(HttpContext context)
    {
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var cultureInfo = new CultureInfo("en");
        
        if (!string.IsNullOrEmpty(requestedCulture) && supportedCultures.Exists(languages => languages.Name.Equals(requestedCulture, StringComparison.OrdinalIgnoreCase)))
        { 
            cultureInfo = new CultureInfo(requestedCulture);
        }
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        await _next(context);
    }
}