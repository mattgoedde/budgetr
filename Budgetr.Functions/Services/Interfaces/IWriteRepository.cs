namespace Budgetr.Functions.Services.Interfaces;

public interface IWriteRepository<in J, K>
    where J : class
    where K : class
{
    Task<K> CreateAsync(Guid userId, J entity);
    Task<K> UpdateAsync(Guid userId, Guid id, J entity);
    Task DeleteAsync(Guid userId, Guid id);
}
