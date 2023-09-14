namespace Budgetr.Ui.Authentication.Models;

public class ClientPrincipal
{
    public string? IdentityProvider { get; init; }
    public string? UserId { get; init; }
    public string? UserDetails { get; init; }
    public IEnumerable<string>? UserRoles { get; init; }
}