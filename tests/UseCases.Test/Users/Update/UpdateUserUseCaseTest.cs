using CashFlow.Application.UseCases.Users.Update;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.User;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using Shouldly;

namespace UseCases.Test.Users.Update;

public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task UpdateUserProfile_Success()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);
        await act.ShouldNotThrowAsync();
        user.Name.ShouldBe(request.Name);
        user.Email.ShouldBe(request.Email);
    }

    [Fact]
    private async Task Error_Name_Empty()
    {
        var user = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.Name = string.Empty;
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
    }

    [Fact]
    private async Task Email_Already_Exists()
    {
    var user = UserBuilder.Build();
    var request = RequestUpdateUserJsonBuilder.Build();
    var useCase = CreateUseCase(user, request.Email);
    var act = async () => await useCase.Execute(request);
    await act.ShouldThrowAsync<ErrorOnValidationException>();
}

    private UpdateUserProfileUseCase CreateUseCase(User user, string? email = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var updateRepository =  UserUpdateOnlyRepositoryBuilder.Build(user);
        var loggedUser = LoggedUserBuilder.Build(user);
        var readRepository = new UsersReadOnlyRepositoryBuilder();

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            readRepository.ExistsActiveUserWithEmail(email);
        }
        
        return new UpdateUserProfileUseCase(
            loggedUser,
            updateRepository,
            readRepository.Build(),
            unitOfWork
        );
        
    }
}