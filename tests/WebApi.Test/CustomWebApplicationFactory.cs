using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infra.DataAccess;
using CommonTestUtils.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>
{
    private User _user;
    private string _password;
    private string _token;

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
            
            StartDatabase(dbContext, passwordEncrypter);
            var tokenEncrypter = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();
            _token = tokenEncrypter.Generate(_user);
        });
    }
    
    public string GetEmail() => _user.Email;
    public string GetName() => _user.Name;
    public string GetPassword() => _password;
    public string GetToken() => _token;
    
    
    private void StartDatabase(CashFlowDbContext dbContext, IPasswordEncrypter passwordEncrypter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;
        _user.Password = passwordEncrypter.Encrypt(_user.Password);
        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}