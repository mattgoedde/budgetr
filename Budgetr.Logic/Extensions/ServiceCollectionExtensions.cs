using Budgetr.Logic.Validators;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Budgetr.Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedValidators(this IServiceCollection services)
    {
        return services
                .AddScoped<IValidator<Budget>, BudgetValidator>()
                .AddScoped<IValidator<Income>, IncomeValidator>()
                .AddScoped<IValidator<Deduction>, DeductionValidator>()
                .AddScoped<IValidator<Expense>, ExpenseValidator>()
                .AddScoped<IValidator<AmortizedLoan>, AmortizedLoanValidator>();
    }

    public static IServiceCollection AddTransientValidators(this IServiceCollection services)
    {
        return services
                .AddTransient<IValidator<Budget>, BudgetValidator>()
                .AddTransient<IValidator<Income>, IncomeValidator>()
                .AddTransient<IValidator<Deduction>, DeductionValidator>()
                .AddTransient<IValidator<Expense>, ExpenseValidator>()
                .AddTransient<IValidator<AmortizedLoan>, AmortizedLoanValidator>();
    }

    public static IServiceCollection AddSingletonValidators(this IServiceCollection services)
    {
        return services
                .AddSingleton<IValidator<Budget>, BudgetValidator>()
                .AddSingleton<IValidator<Income>, IncomeValidator>()
                .AddSingleton<IValidator<Deduction>, DeductionValidator>()
                .AddSingleton<IValidator<Expense>, ExpenseValidator>()
                .AddSingleton<IValidator<AmortizedLoan>, AmortizedLoanValidator>();
    }

    public static IServiceCollection ConfigureBudgetrApi(this IServiceCollection services, Uri baseAddress)
    {
        return services.ConfigureRefitClient<IBudgetrApi>((provider, client) =>
        {
            client.BaseAddress = baseAddress;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // client.DefaultRequestHeaders.Add("x-functions-key", Environment.GetEnvironmentVariable(Constants.EnvironmentVariableTokens.FunctionsKey)!);
        });
    }

    public static IServiceCollection ConfigureRefitClient<TClient>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient)
        where TClient : class
    {
        services.AddHttpClient(nameof(TClient), configureClient)
        .AddTypedClient(client => RestService.For<TClient>(client));
        return services;
    }
}
