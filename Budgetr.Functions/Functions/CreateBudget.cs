namespace Budgetr.Functions.Functions;

public class CreateBudget
{
    private readonly ILogger<CreateBudget> _logger;
    private readonly IBudgetRepository _budgetService;

    public CreateBudget(ILogger<CreateBudget> logger, IBudgetRepository budgetService)
    {
        _logger = logger;
        _budgetService = budgetService;
    }

    [FunctionName(nameof(CreateBudget))]
    [OpenApiOperation(operationId: "Budget.Create", tags: new[] { "Budget" })]
    // [OpenApiSecurity(schemeName: "function_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)] // openapi: describes function api key in query params
    [OpenApiParameter(name: "userId", Type = typeof(Guid), In = ParameterLocation.Path, Description = "User ID Guid", Required = true)]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(BudgetApiModel))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(Budget), Description = "Budget created successfully")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ValidationException), Description = "Invalid Budget")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "User is unauthorized")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Error encountered")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "budgets/{userId:Guid}")] BudgetApiModel budget, Guid userId)
    {
        try
        {
            _logger.LogInformation("{func} function started", nameof(CreateBudget));

            if (userId == Guid.Empty) return new UnauthorizedResult();

            var newBudget = await _budgetService.CreateAsync(userId, budget);

            return new CreatedResult($"api/budgets/{userId}/{newBudget.Id}", newBudget);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "Validation error in {func} occurred.", nameof(CreateBudget));
            return new BadRequestObjectResult(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(CreateBudget));
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
        finally
        {
            _logger.LogInformation("{func} function completed", nameof(CreateBudget));
        }
    }
}