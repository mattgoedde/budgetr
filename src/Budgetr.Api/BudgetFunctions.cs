using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Budgetr.Class.Entities;
using Budgetr.Class.Enums;
using Budgetr.Class.Models;
using Budgetr.DataAccess;
using Budgetr.Logic.Extensions;
using Budgetr.Shared;
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
    public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "budgets")]HttpRequest req)
    {
        using var timer = new MethodTimeLogger<BudgetFunctions>(_logger);

        _logger.LogInformation("CreateBudget invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);

        var newBudget = await JsonSerializer.DeserializeAsync<Budget>(req.Body);
        
        var validationResult = _validator.Validate(newBudget);
        if (!validationResult.IsValid)
            return new BadRequestObjectResult(validationResult.Errors);

        _logger.LogInformation("Budget validated");

        newBudget = newBudget with { UserId = userId, Id = Guid.NewGuid()};

        var entry = await _db.Budgets.AddAsync(newBudget);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Budget created");

        return new CreatedResult($"/api/budget/{entry.Entity.Id}", entry.Entity);
    }

    [Function("ReadBudgets")]
    public async Task<IActionResult> Read([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets")]HttpRequest req)
    {
        using var timer = new MethodTimeLogger<BudgetFunctions>(_logger);

        _logger.LogInformation("ReadBudgets invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);
        
        var budgets = await _db.Budgets.Where(b => b.UserId == userId).ToArrayAsync();

        return new OkObjectResult(budgets);
    }
    
    [Function("ReadBudgetById")]
    public async Task<IActionResult> ReadById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets/{id:Guid}")]HttpRequest req, Guid id)
    {
        using var timer = new MethodTimeLogger<BudgetFunctions>(_logger);

        _logger.LogInformation("ReadBudgetById invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);
        
        var budget = await _db.Budgets.FindAsync(id);

        if (budget is null) return new NotFoundResult();

        if (budget.UserId != userId) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        return new OkObjectResult(budget);
    }

    [Function("DeleteBudget")]
    public async Task<IActionResult> Delete([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "budgets/{id:Guid}")]HttpRequest req, Guid id)
    {
        using var timer = new MethodTimeLogger<BudgetFunctions>(_logger);

        _logger.LogInformation("DeleteBudget invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);
        
        var budget = await _db.Budgets.FindAsync(id);

        if (budget is null) return new NotFoundResult();

        if (budget.UserId != userId) return new StatusCodeResult(StatusCodes.Status403Forbidden);

        _db.Budgets.Remove(budget);
        await _db.SaveChangesAsync();

        return new NoContentResult();
    }

    [Function("SummarizeBudgets")]
    public async Task<IActionResult> Summarize([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets/summarize")]HttpRequest req)
    {
        using var timer = new MethodTimeLogger<BudgetFunctions>(_logger);

        _logger.LogInformation("SummarizeBudgets invoked");

        var userId = req.SwaUserId();
        if (userId == Guid.Empty) return new ForbidResult();

        _logger.LogDebug("User authenticated {userId}", userId);
        
        var budgets = await _db.Budgets
            .Where(b => b.UserId == userId)
            .Include(b => b.AmortizedLoans)
            .Include(b => b.Expenses)
            .Include(b => b.Incomes).ThenInclude(i => i.Deductions)
            .ToArrayAsync();

        var summaries = budgets.Select(b => 
            new BudgetSummaryModel
            {
                BudgetId = b.Id,
                BudgetName = b.Name,
                Frequency = Frequency.Monthly,
                GrossIncome = b.Incomes.Select(i => i.To(Frequency.Monthly)).Sum(i => i.Amount),
                PreTaxDeductions = b.Incomes.Select(i => i.To(Frequency.Monthly)).SelectMany(i => i.Deductions.Where(d => d.DeductionType == DeductionType.PreTax)).Sum(d => d.Amount),
                TaxDeductions = b.Incomes.Select(i => i.To(Frequency.Monthly)).SelectMany(i => i.Deductions.Where(d => d.DeductionType == DeductionType.Tax)).Sum(d => d.Amount),
                PostTaxDeductions = b.Incomes.Select(i => i.To(Frequency.Monthly)).SelectMany(i => i.Deductions.Where(d => d.DeductionType == DeductionType.PostTax)).Sum(d => d.Amount),
                MortagePayment = b.AmortizedLoans.Where(l => l.LoanType == LoanType.Mortage).Sum(l => l.NextPayment().Total),
                CarPayment = b.AmortizedLoans.Where(l => l.LoanType == LoanType.Car).Sum(l => l.NextPayment().Total),
                CreditCardPayment = b.AmortizedLoans.Where(l => l.LoanType == LoanType.CreditCard).Sum(l => l.NextPayment().Total),
                PersonalLoanPayment = b.AmortizedLoans.Where(l => l.LoanType == LoanType.Personal).Sum(l => l.NextPayment().Total),
                OtherLoanPayment = b.AmortizedLoans.Where(l => l.LoanType == LoanType.Other).Sum(l => l.NextPayment().Total),
                HousingExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
                TransportationExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
                CellularExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
                GroceryExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
                LeisureExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
                MiscellaneousExpenses = b.Expenses.Where(e => e.ExpenseType == ExpenseType.Housing).Select(e => e.To(Frequency.Monthly)).Sum(e => e.Amount),
            }).ToArray();

        return new OkObjectResult(summaries);
    }
}