namespace Budgetr.Class.Entities;

public record User
(
    [JsonProperty(PropertyName = "id")]
    Guid Id,
    [JsonProperty(PropertyName = "firstName")]
    string FirstName,
    [JsonProperty(PropertyName = "lastName")]
    string LastName,
    [JsonProperty(PropertyName = "country")]
    string Country,
    [JsonProperty(PropertyName = "city")]
    string City
)
{
    public static User New() => new(Guid.Empty, "", "", "", "");
}