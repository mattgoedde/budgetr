namespace Budgetr.Class.Entities;

public record Budget
(
    [JsonProperty(PropertyName = "id")]
    Guid Id,
    [JsonProperty(PropertyName = "userId")]
    Guid UserId,
    [JsonProperty(PropertyName = "saveTimeUtc")]
    DateTime SaveTimeUtc,
    [JsonProperty(PropertyName = "comment")]
    string Comment,
    [JsonProperty(PropertyName = "incomes")]
    IEnumerable<Income> Incomes,
    [JsonProperty(PropertyName = "housingLoans")]
    IEnumerable<AmortizedLoan> HousingLoans,
    [JsonProperty(PropertyName = "autoLoans")]
    IEnumerable<AmortizedLoan> AutoLoans,
    [JsonProperty(PropertyName = "otherLoans")]
    IEnumerable<AmortizedLoan> OtherLoans,
    [JsonProperty(PropertyName = "expenses")]
    IEnumerable<Expense> Expenses
);