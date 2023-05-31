namespace Budgetr.Class.Entities;

public record UserPreferences
(
    [JsonProperty(PropertyName = "id")]
    Guid Id
)
{
    public static UserPreferences New() => new(Guid.Empty);
}
