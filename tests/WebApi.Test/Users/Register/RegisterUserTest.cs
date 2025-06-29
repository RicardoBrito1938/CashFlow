using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using CashFlow.Exception;
using CommonTestUtils.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : CashFlowClassFixture(factory)
{
    private const string Method = "api/User";
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var result =  await DoPost(Method, request);
        result.StatusCode.ShouldBe(HttpStatusCode.Created);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        response.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Required(string language)
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var result = await DoPost(Method, request, language: language);
        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();
        errors.Count().ShouldBe(1);
        var expectedMessageError = ResourceErrorMessages.ResourceManager.GetString("NAME_REQUIRED", new CultureInfo("pt-BR"));
        errors.First().GetString().ShouldBe(expectedMessageError);
    }
    
}