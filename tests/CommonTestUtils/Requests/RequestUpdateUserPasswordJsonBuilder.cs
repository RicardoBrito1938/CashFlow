using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtils.Requests;

public class RequestUpdateUserPasswordJsonBuilder
{
    public static RequestUpdateUserPasswordJson Build()
    {
        return new Faker<RequestUpdateUserPasswordJson>()
            .RuleFor(user => user.Password, f => f.Internet.Password())
            .RuleFor(user => user.NewPassword, f => f.Internet.Password(prefix: "!Aa1"));
    }
}