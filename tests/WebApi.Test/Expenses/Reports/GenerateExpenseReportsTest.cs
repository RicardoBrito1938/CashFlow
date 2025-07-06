using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace WebApi.Test.Expenses.Reports.Excel;

public class GenerateExpenseReportsTest: CashFlowClassFixture
{
    private const string Method = "api/Report";

    private readonly string _adminToken;
    private readonly string _userToken;
    private readonly DateTime _expenseDate;
    
    public GenerateExpenseReportsTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _adminToken = factory.User_Admin.GetToken();
        _userToken = factory.User_Team_Member.GetToken();
        _expenseDate = factory.Expense_Admin.GetDate();
    }
    
    [Fact]
    private async Task Success_Pdf()
    {
        var result = await DoGet(requestUri: $"{Method}/pdf?month={_expenseDate:Y}", token: _adminToken);
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();
        body.ShouldNotBeNull();
    }
    
    [Fact]
    private async Task Success_Excel()
    {
        var result = await DoGet(requestUri: $"{Method}/excel?month={_expenseDate:Y}", token: _adminToken);
        result.StatusCode.ShouldBe(System.Net.HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();
        body.ShouldNotBeNull();
    }
}