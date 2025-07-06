using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Test.Expenses.GetById;

public class GetExpensesByIdTest: CashFlowClassFixture
{
    private const string Method = "api/Expenses/";
    private readonly string _token;
    private readonly long _expenseId;
    
    public GetExpensesByIdTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _expenseId = factory.Expense.GetExpenseId();
    }
    
    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: Method + _expenseId, token: _token);
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("id").GetInt64().ShouldBe(_expenseId);
    }
    
}