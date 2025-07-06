using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Exception;
using CommonTestUtils.Requests;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Register;

public class RegisterExpenseTest: CashFlowClassFixture
{
    private const string Method = "api/Expenses";
    private readonly string _token;
    
    public RegisterExpenseTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }
    
    [Fact]
    private async Task Success()
    {
       var request = RequestExpenseJsonBuilder.Build();
       var result = await DoPost(requestUri:Method, request: request, token: _token);
       result.StatusCode.ShouldBe(HttpStatusCode.Created);
       var body = await result.Content.ReadAsStreamAsync();
       var response = await JsonDocument.ParseAsync(body);
       response.RootElement.GetProperty("title").GetString().ShouldBe(request.Title);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Required(string language)
    {
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        var result = await DoPost(requestUri:Method, request:request, language: language, token: _token);
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        var expectedError =
            ResourceErrorMessages.ResourceManager.GetString("TITLE_REQUIRED", new CultureInfo(language));
    }
}