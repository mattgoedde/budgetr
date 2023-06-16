namespace Budgetr.Class.Entities;

public interface IAmortizedLoan
{
    string Name { get; }
    double LoanAmount { get; }
    double RemainingBalance { get; }
    double AnnualInterestRate { get; }
    int LoanTermMonths { get; }
}

public record AmortizedLoan : IAmortizedLoan
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; init; } = string.Empty;
    [JsonProperty(PropertyName = "loanAmount")]
    public double LoanAmount { get; init; }
    [JsonProperty(PropertyName = "remainingBalance")]
    public double RemainingBalance { get; init; }
    [JsonProperty(PropertyName = "annualInterestRate")]
    public double AnnualInterestRate { get; init; }
    [JsonProperty(PropertyName = "loanTermMonths")]
    public int LoanTermMonths { get; init; }
}
