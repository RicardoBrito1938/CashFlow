using CashFlow.Application.UseCases.Expenses.Update;
using CashFlow.Domain.Entities;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using CommonTestUtils.Entities;
using CommonTestUtils.LoggedUser;
using CommonTestUtils.Mapper;
using CommonTestUtils.Repositories;
using CommonTestUtils.Requests;
using Shouldly;

namespace UseCases.Test.Expenses.Update;

public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user);
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(user, expense);
        await useCase.Execute(expense.Id, request);
        
        expense.Title.ShouldBe(request.Title);
        expense.Description.ShouldBe(request.Description);
    }
    
    [Fact]
    public async Task Error_Title_Required()
    {
        var user = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(user);
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = string.Empty; // Making title empty to trigger validation error
        var useCase = CreateUseCase(user, expense);
        var act = async () => await useCase.Execute(expense.Id, request);
        var result = await act.ShouldThrowAsync<ErrorOnValidationException>();
    }
    
    [Fact]
    public async Task Errro_Expense_Not_Found()
    {
        var user = UserBuilder.Build();
        var request = RequestExpenseJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(id: 1000, request);
        var result = await act.ShouldThrowAsync<NotFoundException>();
        result.Message.ShouldBe(ResourceErrorMessages.NOT_FOUND);
    }
    
    private UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpenseUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var unitOfWork =  UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        return new UpdateExpenseUseCase(repository, mapper, unitOfWork, loggedUser);
    }

}


