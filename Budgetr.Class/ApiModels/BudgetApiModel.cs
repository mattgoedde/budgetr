namespace Budgetr.Class.ApiModels;

public class BudgetApiModel
{
    [JsonProperty("comment")]
    public string? Comment { get; set; }
    [JsonProperty("incomes")]
    public IEnumerable<Income>? Incomes { get; set; }
    [JsonProperty("housingLoans")]
    public IEnumerable<AmortizedLoan>? HousingLoans { get; set; }
    [JsonProperty("autoLoans")]
    public IEnumerable<AmortizedLoan>? AutoLoans { get; set; }
    [JsonProperty("otherLoans")]
    public IEnumerable<AmortizedLoan>? OtherLoans { get; set; }
    [JsonProperty("expenses")]
    public IEnumerable<Expense>? Expenses { get; set; }
}
