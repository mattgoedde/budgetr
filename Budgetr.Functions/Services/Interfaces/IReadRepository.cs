namespace Budgetr.Functions.Services.Interfaces;

public interface IReadRepository<T>
    where T : class
{
    Task<T?> GetAsync(Guid id, Guid userId);
    Task<IEnumerable<T>> GetAsync(IEnumerable<Guid> ids, Guid userId);
    Task<IEnumerable<T>> GetAllAsync(Guid userId);
}
