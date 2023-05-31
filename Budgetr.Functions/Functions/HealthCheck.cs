namespace Budgetr.Functions.Functions;

public class HealthCheck
{
    private readonly ILogger<HealthCheck> _logger;

    public HealthCheck(ILogger<HealthCheck> log)
    {
        _logger = log;
    }

    [FunctionName(nameof(HealthCheck))]
    [OpenApiOperation(operationId: "Health.Check", tags: new[] { "General" })]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req)
    {
        try
        {
            _logger.LogInformation("{func} function started", nameof(HealthCheck));

            return new OkObjectResult("Healthy");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in health check");
            return new BadRequestResult();
        }
        finally
        {
            _logger.LogInformation("{func} function completed", nameof(HealthCheck));
        }
    }
}

