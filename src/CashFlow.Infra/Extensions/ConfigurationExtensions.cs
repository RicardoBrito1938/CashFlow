using Microsoft.Extensions.Configuration;

namespace CashFlow.Infra.Extensions;

public static class ConfigurationExtensions
{
    public static bool IsTestingEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryTest");
    }
}