using System.Net;
using System.Text.Json;
using CommonTestUtils.Requests;
using Shouldly;

namespace WebApi.Test.Users.Update;

public class UpdateUserTest:CashFlowClassFixture
{
    private const string Method = "api/User";
    private readonly string _token;
    
    public UpdateUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }
    
    [Fact]
    public async Task Success_Update_User_Profile()
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        var response = await DoPut(Method, request, token: _token);
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}