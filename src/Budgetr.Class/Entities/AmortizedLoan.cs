using System.ComponentModel.DataAnnotations;
using Budgetr.Class.Enums;

namespace Budgetr.Class.Entities;

public record AmortizedLoan : BudgetEntity
{
    public string Name { get; set; } = string.Empty;
    public LoanType LoanType { get; set; }
    public double Principal { get; set; }
    public double Balance { get; set; }
    public double AnnualInterestRate { get; set; }
    public int LoanTermMonths { get; set; }
}
