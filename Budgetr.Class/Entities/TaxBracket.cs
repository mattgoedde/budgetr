namespace Budgetr.Class.Entities;

public record TaxBracket
(
    [JsonProperty(PropertyName = "id")]
    Guid Id,
    [JsonProperty(PropertyName = "parentIds")]
    IEnumerable<Guid> ParentIds,
    [JsonProperty(PropertyName = "childIds")]
    IEnumerable<Guid> ChildIds,
    [JsonProperty(PropertyName = "jurisdictionName")]
    string JurisdictionName,

    [JsonProperty(PropertyName = "filingStatus")]
    FilingStatus FilingStatus,
    [JsonProperty(PropertyName = "bracketRate")]
    double BracketRate,
    [JsonProperty(PropertyName = "incomeMinimum")]
    double IncomeMinimum,
    [JsonProperty(PropertyName = "incomeMaximum")]
    double IncomeMaximum,
    [JsonProperty(PropertyName = "previousBracketPayment")]
    double PreviousBracketPayment
)
{
    public static TaxBracket Empty() => new(Guid.Empty, Enumerable.Empty<Guid>(), Enumerable.Empty<Guid>(), "None", FilingStatus.Unknown, 0, 0, 0, 0);
}