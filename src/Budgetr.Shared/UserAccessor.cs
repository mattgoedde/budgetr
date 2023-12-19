using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Budgetr.Shared;

public record User
{
    public string? GivenName { get; init; }
    public string? Surname { get; init; }
    public Guid? ObjectId { get; init; }

    public string? FullName => $"{GivenName} {Surname}";
    public string? LexicalName => $"{Surname}, {GivenName}";
}

public static class UserAccessor
{
    public static async Task<User?> GetUser(this AuthenticationStateProvider authStateProvider)
    {
        try
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            var givenName = authState?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var surname = authState?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var objectId = authState?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(objectId, out var oid))
            {
                return new User
                {
                    GivenName = givenName,
                    Surname = surname,
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

    public static async Task<Guid?> GetObjectId(this AuthenticationStateProvider authStateProvider)
    {
        try
        {
            var authState = await authStateProvider.GetAuthenticationStateAsync();

            var objectId = authState?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(objectId, out var oid))
            {
                return oid;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }
}