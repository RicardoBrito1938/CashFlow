using System.Text;
using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infra;
using CashFlow.Infra.DataAccess;
using CashFlow.Infra.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(options =>
    options.Filters.Add<ExceptionFilter>()
);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var signingKey = builder.Configuration.GetValue<string>("Settings:Jwt:SigningKey");
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = new TimeSpan(0),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!))
    };
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
    
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDatabase(app.Services);

app.Run();
return;

async Task MigrateDatabase(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
}

public abstract partial class Program { }