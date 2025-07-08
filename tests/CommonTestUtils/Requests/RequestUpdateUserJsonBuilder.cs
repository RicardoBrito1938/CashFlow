using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtils.Requests;

public class RequestUpdateUserJsonBuilder
{
    public static RequestUpdateUserProfileJson Build()
    {
        return new Faker<RequestUpdateUserProfileJson>()
            .RuleFor(user => user.Name, f => f.Person.FullName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
    }
}