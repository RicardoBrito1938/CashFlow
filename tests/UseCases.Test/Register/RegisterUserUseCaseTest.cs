using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Cryptography;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using CommonTestUtils.Token;
using Shouldly;

namespace UseCases.Test.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
        result.Token.ShouldNotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task Error_Name_Required()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);
        
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
        result.ShouldNotBeNull();
        result.GetErrors()
            .ShouldContain(error => error == ResourceErrorMessages.NAME_REQUIRED);
    }
    
    [Fact]
    public async Task Email_Already_Exist()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);
        
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
        result.GetErrors()
            .ShouldContain(error => error == ResourceErrorMessages.EMAIL_ALREADY_EXISTS);
    }
    

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncrypter = PasswordEncrypterBuilder.Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var usersWriteOnlyRepository =  UserWriteOnlyRepositoryBuilder.Build();
        var usersReadOnlyRepository = new UsersReadOnlyRepositoryBuilder();
        
        if(string.IsNullOrWhiteSpace(email) == false) usersReadOnlyRepository.ExistsActiveUserWithEmail(email);
        
        return new RegisterUserUseCase(mapper,passwordEncrypter,usersReadOnlyRepository.Build(),usersWriteOnlyRepository, unitOfWork,accessTokenGenerator);
    }
}