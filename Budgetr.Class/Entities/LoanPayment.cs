namespace Budgetr.Class.Entities;

public record LoanPayment
(
    [JsonProperty(PropertyName = "interest")]
    double Interest,
    [JsonProperty(PropertyName = "principal")]
    double Principal
)
{
    public static LoanPayment New() => new(0, 0);
}