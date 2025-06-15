using System.Net;

namespace CashFlow.Exception.ExceptionsBase;

public class InvalidLoginException() : CashFlowException(ResourceErrorMessages.PASSWORD_OR_EMAIL_INVALID)
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}