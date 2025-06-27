using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using Shouldly;

namespace UseCases.Test.Expenses.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        
        var result = await useCase.Execute(request);
        result.ShouldNotBeNull();
        result.Title.ShouldBe(request.Title);
    }
    
    [Fact]
    public async Task Error_TitleRequired()
    {
        var user = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty;
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);
        
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
        result.ShouldNotBeNull();
        result.GetErrors()
            .ShouldContain(error => error == ResourceErrorMessages.TITLE_REQUIRED);
    }
    private RegisterExpenseUseCase CreateUseCase(User user)
    {
        var repository = ExpensesWriteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        
        return new RegisterExpenseUseCase(repository, unitOfWork, mapper, loggedUser);
    }
}