using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infra.DataAccess;
using CommonTestUtils.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
    public UserIdentityManager User_Team_Member { get; private set; }
    public UserIdentityManager User_Admin { get; private set; }
    public ExpenseIdentityManager Expense_Member_Team { get; private set; }
    public ExpenseIdentityManager Expense_Admin { get; private set; }
    

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing").ConfigureServices(services =>
        {
            var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            services.AddDbContext<CashFlowDbContext>(config =>
            {
                config.UseInMemoryDatabase("InMemoryDbForTesting");
                config.UseInternalServiceProvider(provider);
            });

            var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CashFlowDbContext>();
            var passwordEncrypter = scope.ServiceProvider.GetRequiredService<IPasswordEncrypter>();
            var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
            
            StartDatabase(dbContext, passwordEncrypter, accessTokenGenerator);
        });
    }
    
    private void StartDatabase(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator accessTokenGenerator)
    {
        var userTeamMember = AddUserTeamMember(dbContext, passwordEncrypter, accessTokenGenerator);
        var expenseTeamMember = AddExpenses(dbContext, userTeamMember, expenseId: 1, tagId: 1);
        Expense_Member_Team = new ExpenseIdentityManager(expenseTeamMember);
        
        var userAdmin = AddUserAdmin(dbContext, passwordEncrypter, accessTokenGenerator);
        var expenseAdmin = AddExpenses(dbContext, userAdmin, expenseId: 2, tagId: 2);
        Expense_Admin = new ExpenseIdentityManager(expenseAdmin);
        
        dbContext.SaveChanges();
    }
    
    private User AddUserTeamMember(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter,IAccessTokenGenerator tokenEncrypter)
    {
        var user = UserBuilder.Build();
        user.Id = 1; // Ensure the user has a specific ID for testing purposes
        var password = user.Password;
        user.Password = passwordEncrypter.Encrypt(user.Password);
        dbContext.Users.Add(user);
        var token = tokenEncrypter.Generate(user);
        User_Team_Member = new UserIdentityManager(user, password, token);
        return user;
    }
    
    private User AddUserAdmin(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter, IAccessTokenGenerator tokenEncrypter)
    {
        var user = UserBuilder.Build(Roles.ADMIN);        var password = user.Password;
        user.Id = 2; // Ensure the user has a specific ID for testing purposes
        user.Password = passwordEncrypter.Encrypt(user.Password);
        dbContext.Users.Add(user);
        var token = tokenEncrypter.Generate(user);
        User_Admin = new UserIdentityManager(user, password, token);
        return user;
    }
    
    private Expense AddExpenses(CashFlowDbContext dbContext, User user, long expenseId, long tagId)
    {
        var expense = ExpenseBuilder.Build(user);
        expense.Id = expenseId; // Ensure the expense has a specific ID for testing purposes
        foreach (var tag in expense.Tags)
        {
            tag.Id = tagId; // Ensure the tags have a specific ID for testing purposes
            tag.ExpenseId = expense.Id; // Ensure the tags are associated with the correct expense
        }
        dbContext.Expenses.AddRange(expense);
        return expense;
    }
}