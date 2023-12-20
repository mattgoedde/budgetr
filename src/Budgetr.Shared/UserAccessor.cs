using Microsoft.AspNetCore.Components.Authorization;

namespace Budgetr.Shared;

public record User
{
    public string? Name { get; init; }
    public Guid? ObjectId { get; init; }
}

public static class UserAccessor
{
    public const string ObjectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    public const string NameClaimType = "name";

    public static User? AsUser(this AuthenticationState authState)
    {
        try
        {
            var objectId = authState.User?.Claims.FirstOrDefault(c => c.Type == ObjectIdClaimType)?.Value;

            if (Guid.TryParse(objectId, out var oid))
            {
                return new User
                {
                    Name = authState.User?.Claims.FirstOrDefault(c => c.Type == NameClaimType)?.Value,
                    ObjectId = oid
                };
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public static async Task<User?> GetUser(this AuthenticationStateProvider authStateProvider)
    {
        try
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            return authState.AsUser();
        }
        catch
        {
            return null;
        }
    }

    public static async Task<Guid?> GetObjectId(this AuthenticationStateProvider authStateProvider)
    {
        try
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            return authState.AsUser()?.ObjectId;
        }
        catch
        {
            return null;
        }
    }
}