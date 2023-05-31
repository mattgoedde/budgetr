namespace Budgetr.Class.Entities;

public record AmortizedLoan
(
    [JsonProperty(PropertyName = "name")]
    string Name,
    [JsonProperty(PropertyName = "loanAmount")]
    double LoanAmount,
    [JsonProperty(PropertyName = "remainingBalance")]
    double RemainingBalance,
    [JsonProperty(PropertyName = "annualInterestRate")]
    double AnnualInterestRate,
    [JsonProperty(PropertyName = "loanTermYears")]
    int LoanTermYears
)
{
    public static AmortizedLoan New() => new("New Loan", 0, 0, 0, 0);
}