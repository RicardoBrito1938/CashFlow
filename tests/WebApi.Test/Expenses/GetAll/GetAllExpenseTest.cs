using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Test.Expenses.GetAll;

public class GetAllExpenseTest: CashFlowClassFixture
{
    private const string Method = "api/Expenses";
    private readonly string _token;
    
    public GetAllExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.GetToken();
    }
   
    [Fact]
    public async Task Success()
    {
        var result = await DoGet(requestUri: Method, token: _token, language:"pt-BR");
        result.StatusCode.ShouldBe(HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("expenses").GetArrayLength().ShouldBeGreaterThan(0);
    }
}