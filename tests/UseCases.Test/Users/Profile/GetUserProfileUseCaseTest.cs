using CashFlow.Application.UseCases.Users.Porfile;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using Shouldly;

namespace UseCases.Test.Users.Profile;

public class GetUserProfileUseCaseTest
{
   
   [Fact]
   public async Task Success_Get_User_Profile()
   {
      var user = UserBuilder.Build();
      var useCase = CreateUseCase(user);
      var response = await useCase.Execute();
      response.ShouldNotBeNull();
      response.Name.ShouldBe(user.Name);
      response.Email.ShouldBe(user.Email);
   }
   

   private GetUserProfileUseCase CreateUseCase(User user)
   {
      var mapper = MapperBuilder.Build();
      var loggedUser = LoggedUserBuilder.Build(user);
      return new GetUserProfileUseCase(loggedUser, mapper);
   }
}