namespace Budgetr.Functions.Functions;

public class GetLatestBudget
{
    private readonly ILogger<GetLatestBudget> _logger;
    private readonly IBudgetRepository _budgetService;

    public GetLatestBudget(ILogger<GetLatestBudget> logger, IBudgetRepository budgetService)
    {
        _logger = logger;
        _budgetService = budgetService;
    }

    [FunctionName(nameof(GetLatestBudget))]
    [OpenApiOperation(operationId: "Budget.GetLatest", tags: new[] { "Budget" })]
    // [OpenApiSecurity(schemeName: "function_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)] // openapi: describes function api key in query params
    [OpenApiParameter(name: "userId", Type = typeof(Guid), In = ParameterLocation.Path, Description = "User ID Guid", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Budget), Description = "Budget found successfully")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Budget not found")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "User is unauthorized")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Error encountered")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "budgets/{userId:Guid}/latest")] HttpRequest req, Guid userId)
    {
        try
        {
            _logger.LogInformation("{func} function started", nameof(GetLatestBudget));

            if (userId == Guid.Empty) return new UnauthorizedResult();

            var budget = await _budgetService.GetLatestAsync(userId);

            if (budget is null) return new NotFoundResult();

            return new OkObjectResult(budget);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(GetLatestBudget));
            return new BadRequestObjectResult(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(GetLatestBudget));
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
        finally
        {
            _logger.LogInformation("{func} function completed", nameof(GetLatestBudget));
        }
    }
}