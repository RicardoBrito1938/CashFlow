using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Communication.Requests;
using Shouldly;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest(CustomWebApplicationFactory factory): IClassFixture<CustomWebApplicationFactory>
{
    private const string Method = "api/Login";
    private readonly HttpClient _client = factory.CreateClient();
    private readonly string  _email = factory.GetEmail();
    private readonly string _name = factory.GetName();
    private readonly string _password = factory.GetPassword();
    
    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };
        var response = await _client.PostAsJsonAsync(Method, request);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var responseBody = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBody);
        responseData.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
        responseData.RootElement.GetProperty("name").GetString().ShouldBe(_name);
    }
}