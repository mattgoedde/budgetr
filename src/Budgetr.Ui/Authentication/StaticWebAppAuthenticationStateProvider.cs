using System.Net.Http.Json;
using System.Security.Claims;
using Budgetr.Ui.Authentication.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Budgetr.Ui.Authentication;

public class StaticWebAppAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _http;


    public StaticWebAppAuthenticationStateProvider(IWebAssemblyHostEnvironment environment)
    {
        _http = new HttpClient { BaseAddress = new Uri(environment.BaseAddress) };
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var data = await _http.GetFromJsonAsync<AuthenticationModel>("/.auth/me");

            if (data?.ClientPrincipal is null) return new AuthenticationState(new ClaimsPrincipal());
            // if user does not have any roles except for anonymous (user is not signed in)
            if (!data?.ClientPrincipal?.UserRoles?.Where(r => !r.Equals("anonymous", StringComparison.CurrentCultureIgnoreCase)).Any() ?? true) 
                return new AuthenticationState(new ClaimsPrincipal());

            var identity = new ClaimsIdentity(data?.ClientPrincipal.IdentityProvider);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, data?.ClientPrincipal?.UserId ?? ""));
            identity.AddClaim(new Claim(ClaimTypes.Name, data?.ClientPrincipal?.UserDetails ?? ""));
            identity.AddClaims(data?.ClientPrincipal?.UserRoles?
                .Where(r => !r.Equals("anonymous", StringComparison.CurrentCultureIgnoreCase))?
                .Select(r => new Claim(ClaimTypes.Role, r)) ?? Array.Empty<Claim>()
            );
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}