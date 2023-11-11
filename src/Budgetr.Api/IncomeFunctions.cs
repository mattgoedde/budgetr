using System;
using System.Text.Json;
using System.Threading.Tasks;
using Budgetr.Class.Entities;
using Budgetr.DataAccess;
using Budgetr.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Budgetr.Api;

public class IncomeFunctions
{
    private readonly ILogger<IncomeFunctions> _logger;
    private readonly BudgetrDbContext _db;
    private readonly IValidator<Income> _validator;

    public IncomeFunctions(ILogger<IncomeFunctions> logger, BudgetrDbContext db, IValidator<Income> validator)
    {
        _logger = logger;
        _db = db;
        _validator = validator;
    }

    [Function("CreateIncome")]
    public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "budgets/{budgetId:guid}/incomes")]HttpRequest req, Guid budgetId)
    {
        using var timer = new MethodTimeLogger<IncomeFunctions>(_logger);

        _logger.LogInformation("CreateIncome invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);

        var newIncome = await JsonSerializer.DeserializeAsync<Income>(req.Body);
        
        var validationResult = _validator.Validate(newIncome);
        if (!validationResult.IsValid)
            return new BadRequestObjectResult(validationResult.Errors);

        _logger.LogInformation("Budget validated");

        newIncome = newIncome with { Id = Guid.NewGuid()};

        var entry = await _db.Incomes.AddAsync(newIncome);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Income created");

        return new CreatedResult($"/api/budgets/{budgetId}/incomes/{entry.Entity.Id}", entry.Entity);
    }
}
