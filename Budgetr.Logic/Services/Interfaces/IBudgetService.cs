using Refit;

namespace Budgetr.Logic.Services.Interfaces;

public interface IBudgetService
{
    [Post(path: "/api/budgets/{userId}")]
    Task<Budget?> CreateAsync(Guid userId, [Body] BudgetApiModel budget);
    
    [Patch(path: "/api/budgets/{userId}/{id}")]
    Task<Budget?> UpdateAsync(Guid userId, Guid id, [Body] BudgetApiModel budget);

    [Delete(path: "/api/budgets/{userId}/{id}")]
    Task DeleteAsync(Guid userId, Guid id);

    [Get(path: "/api/budgets/{userId}/{id}")]
    Task<Budget?> GetAsync(Guid userId, Guid id);

    [Get(path: "/api/budgets/{userId}/latest")]
    Task<Budget?> GetLatestAsync(Guid userId);

    [Get(path: "/api/budgets/{userId}")]
    Task<IEnumerable<Budget>> GetAllAsync(Guid userId);
}
