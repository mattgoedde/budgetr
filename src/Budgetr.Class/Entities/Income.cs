using Budgetr.Class.Enums;
using Budgetr.Class.Extensions;

namespace Budgetr.Class.Entities;

public record Income : BudgetEntity
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public Frequency Frequency { get; set; }
    public ICollection<Deduction>? Deductions { get; set; }

    public double NetAmount => Amount - (Deductions?.Select(d => d.To(Frequency)).Sum(d => d.Amount) ?? 0);
    
    public double PreTaxDeductions => Deductions?.Where(d => d.DeductionType == DeductionType.PreTax).Sum(d => d.Amount) ?? 0;
    public double TaxDeductions => Deductions?.Where(d => d.DeductionType == DeductionType.Tax).Sum(d => d.Amount) ?? 0;
    public double PostTaxDeductions => Deductions?.Where(d => d.DeductionType == DeductionType.PostTax).Sum(d => d.Amount) ?? 0;
}