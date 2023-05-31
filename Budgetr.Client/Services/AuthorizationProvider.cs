using Budgetr.Class.Entities;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Newtonsoft.Json;
using System.Text;

namespace Budgetr.Client.Services;

public class AuthorizationProvider : IAuthorizationProvider
{
    private readonly ILogger<AuthorizationProvider> _logger;
    private readonly IAccessTokenProvider _accessTokenProvider;

    private const string USER_ID_KEY = "sub";
    private const string CITY_KEY = "city";
    private const string COUNTRY_KEY = "country";
    private const string GIVEN_NAME_KEY = "given_name";
    private const string FAMILY_NAME_KEY = "family_name";

    public AuthorizationProvider(ILogger<AuthorizationProvider> logger, IAccessTokenProvider accessTokenProvider)
    {
        _logger = logger;
        _accessTokenProvider = accessTokenProvider;
    }

    public async Task<User?> GetUserAsync()
    {
        try
        {
            _logger.LogTrace("Getting user");
            var accessToken = await GetAccessToken();
            
            if (accessToken is null) return null;
            
            _ = Guid.TryParse(accessToken[USER_ID_KEY].ToString(), out Guid userId);
            string country = accessToken[COUNTRY_KEY].ToString() ?? "";
            string city = accessToken[CITY_KEY].ToString() ?? "";
            string firstName = accessToken[GIVEN_NAME_KEY].ToString() ?? "";
            string lastName = accessToken[FAMILY_NAME_KEY].ToString() ?? "";
            
            return new User(Id: userId, FirstName: firstName, LastName: lastName, Country: country, City: city);
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Exception getting user");
            return null;
        }
    }

    public async Task<Guid?> GetUserIdAsync()
    {
        try
        {
            _logger.LogTrace("Getting User Id");
            var accessToken = await GetAccessToken();
            if (accessToken is null) return null;
            if (Guid.TryParse(accessToken[USER_ID_KEY].ToString(), out Guid result))
            {
                _logger.LogTrace("User {guid} found", result);
            }
            else
            {
                _logger.LogTrace("User not found", result);
            }
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Exception getting user id");
            return null;
        }
    }

    private async Task<IDictionary<string, object>?> GetAccessToken()
    {
        var accessTokenResult = await _accessTokenProvider.RequestAccessToken();
        if (!accessTokenResult.TryGetToken(out var token))
        {
            throw new InvalidOperationException("Failed to provision the access token.");
        }

        // header.payload.signature
        var payload = token.Value.Split(".")[1];
        var base64Payload = payload.Replace('-', '+').Replace('_', '/').PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
        var data = Convert.FromBase64String(base64Payload);
        var decodedString = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<IDictionary<string, object>>(decodedString);
    }
}