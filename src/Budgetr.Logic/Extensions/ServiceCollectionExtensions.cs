﻿using Budgetr.Logic.Validators;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Budgetr.Logic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBudgetrValidators(this IServiceCollection services)
    {
        return services
                .AddSingleton<IValidator<Budget>, BudgetValidator>()
                .AddSingleton<IValidator<Income>, IncomeValidator>()
                .AddSingleton<IValidator<Deduction>, DeductionValidator>()
                .AddSingleton<IValidator<Expense>, ExpenseValidator>()
                .AddSingleton<IValidator<AmortizedLoan>, AmortizedLoanValidator>();
    }

    public static IServiceCollection AddRefitClient<TClient>(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient)
        where TClient : class
    {
        services.AddHttpClient(nameof(TClient), configureClient)
        .AddTypedClient(client => RestService.For<TClient>(client));
        return services;
    }
}
