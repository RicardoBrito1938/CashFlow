using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infra;
using CashFlow.Infra.DataAccess;
using CashFlow.Infra.Migrations;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(options =>
    options.Filters.Add<ExceptionFilter>()
);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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

app.UseAuthorization();

app.MapControllers();

await MigrateDatabase(app.Services);

app.Run();

async Task MigrateDatabase(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    await DataBaseMigration.MigrateDatabase(scope.ServiceProvider);
}