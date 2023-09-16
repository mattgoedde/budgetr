using System.ComponentModel.DataAnnotations;
using Budgetr.Class.Enums;

namespace Budgetr.Class.Entities;

public interface IAmortizedLoan
{
    string Name { get; }
    LoanType LoanType { get; }
    double Principal { get; }
    double Balance { get; }
    double AnnualInterestRate { get; }
    int LoanTermMonths { get; }
}

public record AmortizedLoan : BudgetEntity, IAmortizedLoan
{
    public string Name { get; set; } = string.Empty;
    public LoanType LoanType { get; set; }
    public double Principal { get; set; }
    public double Balance { get; set; }
    public double AnnualInterestRate { get; set; }
    public int LoanTermMonths { get; set; }
}
