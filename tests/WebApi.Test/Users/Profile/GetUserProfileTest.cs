using System.Net;
using System.Text.Json;
using Shouldly;

namespace WebApi.Test.Users.Profile;

public class GetUserProfileTest: CashFlowClassFixture
{
    private readonly string Method = "api/User";
    private readonly string _token;
    private readonly string _userName;
    private readonly string _userEmail;

    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _userName = factory.User_Team_Member.GetName();
        _userEmail = factory.User_Team_Member.GetEmail();
    }
    
    [Fact]
    public async Task Success_Get_User_Profile()
    {
        var response = await DoGet(Method, _token);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonDocument.ParseAsync(body);
        result.RootElement.TryGetProperty("name", out var name).ShouldBeTrue();
        result.RootElement.TryGetProperty("email", out var email).ShouldBeTrue();
    }
}