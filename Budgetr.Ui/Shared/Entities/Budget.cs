namespace Budgetr.Ui.Shared.Entities;

public record Budget
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public string? Comment { get; init; }
    public IEnumerable<Income>? Incomes { get; init; }
    public IEnumerable<AmortizedLoan>? HousingLoans { get; init; }
    public IEnumerable<AmortizedLoan>? AutoLoans { get; init; }
    public IEnumerable<AmortizedLoan>? OtherLoans { get; init; }
    public IEnumerable<Expense>? Expenses { get; init; }
}