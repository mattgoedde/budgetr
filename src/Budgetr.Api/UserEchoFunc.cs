using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Budgetr.Api;

public class UserEchoFunc
{
    private readonly ILogger<UserEchoFunc> _logger;

    [Function("UserEchoFunc")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
    {
        return new OkObjectResult(req.SwaUserId());
    }
}
