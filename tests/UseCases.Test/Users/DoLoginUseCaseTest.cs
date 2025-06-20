using CashFlow.Application.UseCases.Login;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Cryptography;
using CommonTestUtils.Entities;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using CommonTestUtils.Token;
using Shouldly;

namespace UseCases.Test.Users;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user, request.Password);
        var response = await useCase.Execute(request);
        response.ShouldNotBeNull();
        response.Name.ShouldNotBeNullOrEmpty();
        response.Token.ShouldNotBeNullOrEmpty();
        response.Name.ShouldBe(user.Name);
    }
    
    [Fact]
    public async Task Error_User_Not_Found()
    {
        var request = RequestLoginJsonBuilder.Build();
        var useCase = CreateUseCase(UserBuilder.Build(), request.Password);
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.ShouldThrowAsync<InvalidLoginException>();
        result.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;
        var useCase = CreateUseCase(user, "wrongPassword");
        
        var act = async () => await useCase.Execute(request);
        
        var result = await act.ShouldThrowAsync<InvalidLoginException>();
        result.ShouldNotBeNull();
    }
    
    
    
    private DoLoginUseCase CreateUseCase(User user, string password)
    {
        var passwordEncrypter = new PasswordEncrypterBuilder().Verify(password).Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var repository = new UsersReadOnlyRepositoryBuilder().GetByEmail(user).Build();
        
        return new DoLoginUseCase(repository, passwordEncrypter, accessTokenGenerator);
    }
}