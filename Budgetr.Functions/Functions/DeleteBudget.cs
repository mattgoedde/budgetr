namespace Budgetr.Functions.Functions;

public class DeleteBudget
{
    private readonly ILogger<DeleteBudget> _logger;
    private readonly IBudgetRepository _budgetService;

    public DeleteBudget(ILogger<DeleteBudget> logger, IBudgetRepository budgetService)
    {
        _logger = logger;
        _budgetService = budgetService;
    }

    [FunctionName(nameof(DeleteBudget))]
    [OpenApiOperation(operationId: "Budget.DeleteById", tags: new[] { "Budget" })]
    // [OpenApiSecurity(schemeName: "function_key", schemeType: SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)] // openapi: describes function api key in query params
    [OpenApiParameter(name: "userId", Type = typeof(Guid), In = ParameterLocation.Path, Description = "User ID Guid", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.NoContent, contentType: "application/json", bodyType: typeof(Budget), Description = "Budget deleted successfully")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Budget not found")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest, Description = "Invalid Id")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Unauthorized, Description = "User is unauthorized")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.InternalServerError, Description = "Error encountered")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "budgets/{userId:Guid}/{id:Guid}")] HttpRequest req, Guid userId, Guid id)
    {
        try
        {
            _logger.LogInformation("{func} function started", nameof(DeleteBudget));

            if (userId == Guid.Empty) return new UnauthorizedResult();
            if (id == Guid.Empty) throw new ArgumentException("Budget Id was null");

            await _budgetService.DeleteAsync(id, userId);

            return new NoContentResult();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(DeleteBudget));
            return new BadRequestObjectResult(ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {func} function", nameof(DeleteBudget));
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
        finally
        {
            _logger.LogInformation("{func} function completed", nameof(DeleteBudget));
        }
    }
}