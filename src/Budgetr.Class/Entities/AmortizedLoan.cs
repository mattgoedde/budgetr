using System.ComponentModel.DataAnnotations;

namespace Budgetr.Class.Entities;

public interface IAmortizedLoan
{
    string Name { get; }
    double Principal { get; }
    double Balance { get; }
    double AnnualInterestRate { get; }
    int LoanTermMonths { get; }
}

public record AmortizedLoan : BudgetEntity, IAmortizedLoan
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Loans must have a name!")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "A loan must have a positive starting amount!")]
    public double Principal { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "A loan must have a positive balance!")]
    public double Balance { get; set; }

    [Range(0.01, 1.00, ErrorMessage = "A loan must have an annual interest rate between 0 and 1!")]
    public double AnnualInterestRate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A loan must have a term at least 1 month long!")]
    public int LoanTermMonths { get; set; }
}
