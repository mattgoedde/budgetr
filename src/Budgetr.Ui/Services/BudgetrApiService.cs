using System.Net.Http.Json;
using Budgetr.Class.Entities;
using Budgetr.Class.Models;
using Budgetr.Shared;
using FluentValidation;

namespace Budgetr.Ui.Services;

public class BudgetrApiService
{
    private readonly ILogger<BudgetrApiService> _logger;
    private readonly HttpClient _http;
    private readonly IValidator<Budget> _validator;

    public BudgetrApiService(ILogger<BudgetrApiService> logger, HttpClient http)
    {
        _logger = logger;
        _http = http;
    }

    public async Task CreateBudget(Budget newBudget)
    {
        _validator.ValidateAndThrow(newBudget);

        await _http.PostAsJsonAsync("/api/budgets", newBudget);
    }   

    public async Task<IEnumerable<BudgetSummaryModel>> GetSummaries()
    {
        using var timer = new MethodTimeLogger<BudgetrApiService>(_logger);

        return await _http.GetFromJsonAsync<IEnumerable<BudgetSummaryModel>>("/api/budgets/summarize") ?? Enumerable.Empty<BudgetSummaryModel>();
    }
}