using Budgetr.Class.Enums;

namespace Budgetr.Class.Entities;

public record Expense : BudgetEntity
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public Frequency Frequency { get; set; }
    public ExpenseType ExpenseType { get; set; }
}