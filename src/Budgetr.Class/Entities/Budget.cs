using System.Text.Json.Serialization;

namespace Budgetr.Class.Entities;

public interface IBudget<TIncome, TDeduction, TExpense, TAmortizedLoan>
    where TIncome : IIncome<TDeduction>
    where TDeduction : IDeduction
    where TExpense : IExpense
    where TAmortizedLoan : IAmortizedLoan
{
    Guid UserId { get; }
    string Name { get; }
    ICollection<TIncome> Incomes { get; }
    ICollection<TExpense> Expenses { get; }
    ICollection<TAmortizedLoan> AmortizedLoans { get; }
}

public record Budget : BaseEntity, IBudget<Income, Deduction, Expense, AmortizedLoan>
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("incomes")]
    public ICollection<Income> Incomes { get; set; } = new List<Income>();
    [JsonPropertyName("expenses")]
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    [JsonPropertyName("amortizedLoans")]
    public ICollection<AmortizedLoan> AmortizedLoans { get; set; } = new List<AmortizedLoan>();
}
