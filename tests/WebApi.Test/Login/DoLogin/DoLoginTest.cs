using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CommonTestUtils.Requests;
using Shouldly;
using WebApi.Test.InlineData;

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

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task ErrorLoginInvalid(string language)
    {
       var request = RequestLoginJsonBuilder.Build();
       _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
       var response = await _client.PostAsJsonAsync(Method, request);
         response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
         
         var responseBody = await response.Content.ReadAsStreamAsync();
         var responseData = await JsonDocument.ParseAsync(responseBody);
         var errors = responseData.RootElement.GetProperty("errorMessages").EnumerateArray();
         var expectedMessageError = ResourceErrorMessages.ResourceManager.GetString("PASSWORD_OR_EMAIL_INVALID", new CultureInfo(language));
         errors.First().GetString().ShouldBe(expectedMessageError);
    }
}