using Budgetr.Class.Enums;

namespace Budgetr.Class.Entities;

public interface IIncome<TDeduction> where TDeduction : IDeduction
{
    string Name { get; }
    double Amount { get; }
    Frequency Frequency { get; }
    ICollection<TDeduction> Deductions { get; }
}

public record Income : BudgetEntity, IIncome<Deduction>
{
    public string Name { get; set; } = string.Empty;
    public double Amount { get; set; }
    public Frequency Frequency { get; set; }
    public ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();
}