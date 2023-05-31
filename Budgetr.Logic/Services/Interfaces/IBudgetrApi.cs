using Refit;

namespace Budgetr.Logic.Services.Interfaces;

public interface IBudgetrApi : IBudgetService
{
    [Get("/api/health")]
    Task<string> HealthCheckAsync();
}
