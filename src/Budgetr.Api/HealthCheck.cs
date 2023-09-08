using System.Net;
using Budgetr.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Budgetr.Api;

public class HealthCheck
{
    private readonly ILogger<HealthCheck> _logger;

    public HealthCheck(ILogger<HealthCheck> logger)
    {
        _logger = logger;
    }

    [Function(nameof(HealthCheck))]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "api/HealtCheck")] HttpRequest req)
    {
        using var timer = new MethodTimeLogger<HealthCheck>(_logger);

        return new OkResult();
    }
}
