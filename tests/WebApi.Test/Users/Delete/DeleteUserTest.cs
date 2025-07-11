using System.Net;
using Shouldly;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest: CashFlowClassFixture
{
    private const string Method = "api/user";
    private readonly string _token;
    
    public DeleteUserTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
    }
    
    [Fact]
    public async Task Success()
    {
        var response = await DoDelete(Method, _token);
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        
    }
}