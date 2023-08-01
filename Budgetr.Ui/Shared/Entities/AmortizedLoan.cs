namespace Budgetr.Ui.Shared.Entities;

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
    public string Name { get; init; } = string.Empty;
    public double LoanAmount { get; init; }
    public double RemainingBalance { get; init; }
    public double AnnualInterestRate { get; init; }
    public int LoanTermMonths { get; init; }
}
