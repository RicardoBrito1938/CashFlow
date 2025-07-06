using System.Net;
using CashFlow.Domain.Entities;
using CommonTestUtils.Requests;
using Shouldly;

namespace WebApi.Test.Expenses.Update;

public class UpdateExpenseTest: CashFlowClassFixture
{
    private const string Method = "api/Expenses";
    private readonly string _token;
    private readonly long _expenseId;

    public UpdateExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _expenseId = factory.Expense_Member_Team.GetExpenseId();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = RequestExpenseJsonBuilder.Build();
        var response = await DoPut($"{Method}/{_expenseId}", request, _token);
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}