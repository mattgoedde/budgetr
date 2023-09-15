using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Budgetr.Class.Entities;
using Budgetr.DataAccess;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetr.Api;

public class BudgetFunctions
{
    private readonly ILogger<BudgetFunctions> _logger;
    private readonly BudgetrDbContext _db;
    private readonly IValidator<Budget> _validator;

    public BudgetFunctions(ILogger<BudgetFunctions> logger, BudgetrDbContext db, IValidator<Budget> validator)
    {
        _logger = logger;
        _db = db;
        _validator = validator;
    }

    [Function("CreateBudget")]
    public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "budget")]HttpRequest req)
    {
        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        var newBudget = await JsonSerializer.DeserializeAsync<Budget>(req.Body);
        
        var validationResult = _validator.Validate(newBudget);
        if (!validationResult.IsValid)
            return new BadRequestObjectResult(validationResult.Errors);

        newBudget = newBudget with { UserId = userId };

        var entry = await _db.Budgets.AddAsync(newBudget);
        await _db.SaveChangesAsync();

        return new CreatedResult($"/api/budget/{entry.Entity.Id}", entry.Entity);
    }

    [Function("ReadBudgets")]
    public async Task<IActionResult> Read([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets")]HttpRequest req)
    {
        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new StatusCodeResult(StatusCodes.Status403Forbidden);
        
        var budgets = await _db.Budgets.Where(b => b.UserId == userId).ToArrayAsync();

        return new OkObjectResult(budgets);
    }
    
    [Function("ReadBudgetById")]
    public async Task<IActionResult> ReadById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets/{id:Guid}")]HttpRequest req, Guid id)
    {
        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new StatusCodeResult(StatusCodes.Status403Forbidden);
        
        var budget = await _db.Budgets.FindAsync(id);

        if (budget is null) return new NotFoundResult();

        if (budget.UserId != userId) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        return new OkObjectResult(budget);
    }

    [Function("DeleteBudget")]
    public async Task<IActionResult> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "budget/{id:Guid}")]HttpRequest req, Guid id)
    {
        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new StatusCodeResult(StatusCodes.Status403Forbidden);
        
        var budget = await _db.Budgets.FindAsync(id);

        if (budget is null) return new NotFoundResult();

        if (budget.UserId != userId) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        _db.Budgets.Remove(budget);
        await _db.SaveChangesAsync();

        return new NoContentResult();
    }
}