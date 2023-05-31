using Budgetr.Logic;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Budgetr.Functions.Startup))]

namespace Budgetr.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services
            .AddSingleton<IBudgetRepository, CosmosBudgetService>()
            .AddSingletonValidators();
        builder.Services
            .Configure<CosmosBudgetServiceOptions>(options =>
            {
                options.CosmosEndpoint = Environment.GetEnvironmentVariable(Constants.EnvironmentVariableTokens.CosmosEndpoint)!;
                options.CosmosKey = Environment.GetEnvironmentVariable(Constants.EnvironmentVariableTokens.CosmosKey)!;
            });
    }
}