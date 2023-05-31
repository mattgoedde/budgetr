using Budgetr.Class.Entities;

namespace Budgetr.Client.Services;

public interface IAuthorizationProvider
{
    Task<Guid?> GetUserIdAsync();
    Task<User?> GetUserAsync();
}
