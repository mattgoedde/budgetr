using Budgetr.Class.Enums;
using System.ComponentModel.DataAnnotations;

namespace Budgetr.Class.Entities;

public record Deduction : BudgetEntity
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Deductions must have a name!")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "A deduction must have a positive amount!")]
    public double Amount { get; set; }

    [Range((int)Frequency.Yearly, (int)Frequency.Hourly, ErrorMessage = "A deduction cannot have an unknown frequency")]
    public Frequency Frequency { get; set; }

    [Range((int)DeductionType.PreTax, (int)DeductionType.PostTax, ErrorMessage = "A deduction cannot have an unknown type")]
    public DeductionType DeductionType { get; set; }

    public Guid IncomeId { get; set; }
    public Income Income { get; set; } = null!;
}