using Budgetr.Ui.Shared.Enums;

namespace Budgetr.Ui.Shared.Entities;

public record Deduction
{
    public string? Name { get; init; }
    public double Amount { get; init; }
    public Frequency Frequency { get; init; }
    public DeductionType DeductionType { get; init; }
}