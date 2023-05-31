namespace Budgetr.Functions.Services.Interfaces;

public interface IBudgetRepository : IWriteRepository<BudgetApiModel, Budget>, IReadRepository<Budget>
{
    Task<Budget?> GetLatestAsync(Guid userId);
}
