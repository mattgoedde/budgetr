using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Budgetr.Shared;

namespace Budgetr.Api;

public static class HealthCheck
{
    [FunctionName(nameof(HealthCheck))]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger logger)
    {
        using var timer = new MethodTimeLogger(typeof(HealthCheck), logger);

        return new OkResult();
    }
}
