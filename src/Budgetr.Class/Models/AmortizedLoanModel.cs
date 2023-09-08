using Budgetr.Ui.Shared.Entities;

namespace Budgetr.Ui.Shared.Models;

public class AmortizedLoanModel : IAmortizedLoan
{
    public string Name { get; set; } = string.Empty;
    public double LoanAmount { get; set; } = 0.0;
    public double RemainingBalance { get; set; } = 0.0;
    public double AnnualInterestRate { get; set; } = 0.0;
    public int LoanTermMonths { get; set; } = 1;
}
