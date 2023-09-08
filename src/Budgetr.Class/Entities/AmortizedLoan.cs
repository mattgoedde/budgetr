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
    public string Name { get; set; } = string.Empty;
    public double LoanAmount { get; set; }
    public double RemainingBalance { get; set; }
    public double AnnualInterestRate { get; set; }
    public int LoanTermMonths { get; set; }
}
