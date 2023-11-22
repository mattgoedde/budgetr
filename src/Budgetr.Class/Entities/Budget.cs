namespace Budgetr.Class.Entities;

public record Budget : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Income>? Incomes { get; set; }
    public ICollection<Expense>? Expenses { get; set; }
    public ICollection<AmortizedLoan>? AmortizedLoans { get; set; }
}
