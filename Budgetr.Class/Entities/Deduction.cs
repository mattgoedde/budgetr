
namespace Budgetr.Class.Entities;

public record Deduction
(
    [JsonProperty(PropertyName = "name")]
    string Name,
    [JsonProperty(PropertyName = "amount")]
    double Amount,
    [JsonProperty(PropertyName = "frequency")]
    Frequency Frequency,
    [JsonProperty(PropertyName = "deductionType")]
    DeductionType DeductionType
)
{
    public static Deduction New() => new("New Deduction", 0, Frequency.Unknown, DeductionType.Unknown);
}
