using Budgetr.Class.Enums;

namespace Budgetr.Class.Entities;

public record Income : BudgetEntity
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public Frequency Frequency { get; set; }
    public ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();
}