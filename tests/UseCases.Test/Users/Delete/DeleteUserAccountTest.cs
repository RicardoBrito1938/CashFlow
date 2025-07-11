using CashFlow.Application.UseCases.Users.Delete;
using CashFlow.Domain.Entities;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Repositories;
using Shouldly;

namespace UseCases.Test.Users.Delete;

public class DeleteUserAccountTest
{
    [Fact]
    public async Task Success()
    {
        // Arrange
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        // Act
        var act = async () => await useCase.Execute();
        await act.ShouldNotThrowAsync();


    }
    
    private DeleteUserUseCase CreateUseCase(User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteUserUseCase(unitOfWork, writeOnlyRepository, loggedUser);
    }
}