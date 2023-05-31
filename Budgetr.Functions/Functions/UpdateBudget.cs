namespace Budgetr.Functions.Functions;

public class UpdateBudget
{
    private readonly ILogger<UpdateBudget> _logger;
    private readonly IBudgetRepository _budgetService;

    public UpdateBudget(ILogger<UpdateBudget> logger, IBudgetRepository budgetService)
    {
        _logger = logger;
        _budgetService = budgetService;
    }

    [FunctionName(nameof(UpdateBudget))]
    [OpenApiOperation(operationId: "Budget.Update", tags: new[] { "Budget" })]
    // [OpenApiSecurity(schemeName: "function_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)] // openapi: describes function api key in query params
    [OpenApiParameter(name: "userId", Type = typeof(Guid), In = ParameterLocation.Path, Description = "User ID Guid", Required = true)]
    [OpenApiParameter(name: "id", Type = typeof(Guid), In = ParameterLocation.Path, Description = "Budget ID Guid", Required = true)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BudgetApiModel))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Budget), Description = "Budget updated successfully")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ValidationException), Description = "Invalid Budget")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "User is unauthorized")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Error encountered")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "budgets/{userId:Guid}/{id:Guid}")] BudgetApiModel budget, Guid userId, Guid id)
    {
        try
        {
            _logger.LogInformation("{func} function started", nameof(UpdateBudget));

            if (userId == Guid.Empty) return new UnauthorizedResult();
            if (id == Guid.Empty) throw new ArgumentException("Budget Id was null");

            var updatedBudget = await _budgetService.UpdateAsync(userId, id, budget);

            return new OkObjectResult(updatedBudget);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error in {func} occurred.", nameof(UpdateBudget));
            return new BadRequestObjectResult(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(UpdateBudget));
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
        finally
        {
            _logger.LogInformation("{func} function completed", nameof(UpdateBudget));
        }
    }
}