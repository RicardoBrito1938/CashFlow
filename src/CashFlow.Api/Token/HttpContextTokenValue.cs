using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor contextAccessor): ITokenProvider
{
    public string TokenRequest()
    {
        var authorization = contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}