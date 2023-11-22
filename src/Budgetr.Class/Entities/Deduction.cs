using Budgetr.Class.Enums;
using System.ComponentModel.DataAnnotations;

namespace Budgetr.Class.Entities;

public record Deduction : BudgetEntity
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public Frequency Frequency { get; set; }
    public DeductionType DeductionType { get; set; }
    public Guid IncomeId { get; set; }
    public Income Income { get; set; } = null!;
}