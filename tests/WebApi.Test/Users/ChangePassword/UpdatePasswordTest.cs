using System.Net;
using CashFlow.Communication.Requests;
using CommonTestUtils.Requests;
using Shouldly;

namespace WebApi.Test.Users.ChangePassword;

public class UpdatePasswordTest:CashFlowClassFixture
{
    private const string Method = "api/user/change-password";
    private readonly string _token;
    private readonly string _password;
    private readonly string _email;
    
    public UpdatePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User_Team_Member.GetToken();
        _password = factory.User_Team_Member.GetPassword();
        _email = factory.User_Team_Member.GetEmail();
    }
    
     [Fact]
     public async Task Success()
     {
         var request = RequestUpdateUserPasswordJsonBuilder.Build();
         request.Password = _password;
         var response = await DoPut(Method, request,  _token);
         response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
         var loginRequest = new RequestLoginJson()
         {
             Email = _email,
             Password = _password
         };
         response = await DoPost("api/login",loginRequest);
         response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
         loginRequest.Password = request.NewPassword;
         response = await DoPost("api/login", loginRequest);
         response.StatusCode.ShouldBe(HttpStatusCode.OK);
     }
}