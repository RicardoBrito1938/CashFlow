using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtils.Requests;

public class RequestExpenseJsonBuilder
{
    public static RequestExpenseJson Build()
    { 
        return new Faker<RequestExpenseJson>()
            .RuleFor(rule => rule.Title,faker => faker.Commerce.ProductName())
            .RuleFor(rule => rule.Description,faker => faker.Commerce.ProductDescription())
            .RuleFor(rule => rule.Date,faker => faker.Date.Past(1)) // Date in the past
            .RuleFor(rule => rule.Amount,faker => faker.Finance.Amount(1, 1000))
            .RuleFor(rule => rule.PaymentType,faker => faker.PickRandom<PaymentsType>());
    }
}