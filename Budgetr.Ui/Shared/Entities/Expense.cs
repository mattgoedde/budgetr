using Budgetr.Ui.Shared.Enums;

namespace Budgetr.Ui.Shared.Entities;

public record Expense
{
    public string? Name { get; init; }
    public double Amount { get; init; }
    public Frequency Frequency { get; init; }
    public ExpenseType ExpenseType { get; init; }
}