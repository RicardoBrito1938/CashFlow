using CashFlow.Domain.Entities;
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
    public ExpenseIdentityManager Expense { get; private set; }
    

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
        var user = AddUserTeamMember(dbContext, passwordEncrypter, accessTokenGenerator);
        AddExpenses(dbContext, user);
        dbContext.SaveChanges();
    }
    
    private User AddUserTeamMember(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter,IAccessTokenGenerator tokenEncrypter)
    {
        var user = UserBuilder.Build();
        var password = user.Password;
        user.Password = passwordEncrypter.Encrypt(user.Password);
        dbContext.Users.Add(user);
        var token = tokenEncrypter.Generate(user);
        User_Team_Member = new UserIdentityManager(user, password, token);
        return user;
    }
    
    private void AddExpenses(CashFlowDbContext dbContext, User user)
    {
        var expense = ExpenseBuilder.Build(user);
        dbContext.Expenses.AddRange(expense);
        Expense = new ExpenseIdentityManager(expense);
    }
}