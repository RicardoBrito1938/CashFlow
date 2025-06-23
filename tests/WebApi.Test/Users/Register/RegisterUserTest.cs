using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtils.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace WebApi.Test.Users.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private const string Method = "api/User";
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var result =  await _client.PostAsJsonAsync(Method, request);
        result.StatusCode.ShouldBe(HttpStatusCode.Created);
        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        response.RootElement.GetProperty("token").GetString().ShouldNotBeNullOrEmpty();
    }
}