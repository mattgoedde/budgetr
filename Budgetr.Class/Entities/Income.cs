namespace Budgetr.Class.Entities;

public record Income
(
    [JsonProperty(PropertyName = "name")]
    string Name,
    [JsonProperty(PropertyName = "amount")]
    double Amount,
    [JsonProperty(PropertyName = "frequency")]
    Frequency Frequency,
    [JsonProperty(PropertyName = "deductions")]
    IEnumerable<Deduction> Deductions
)
{
    public static Income New() => new("New Income", 0, Frequency.Unknown, Enumerable.Empty<Deduction>());
}