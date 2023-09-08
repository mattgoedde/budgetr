using System.ComponentModel.DataAnnotations;

namespace Budgetr.Class.Entities;

public interface IBudget<TIncome, TDeduction, TExpense, TAmortizedLoan>
    where TIncome : IIncome<TDeduction>
    where TDeduction : IDeduction
    where TExpense : IExpense
    where TAmortizedLoan : IAmortizedLoan
{
    string Name { get; }
    ICollection<TIncome> Incomes { get; }
    ICollection<TExpense> Expenses { get; }
    ICollection<TAmortizedLoan> AmortizedLoans { get; }
}

public record Budget : BaseEntity, IBudget<Income, Deduction, Expense, AmortizedLoan>
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "You must give your budget a name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Income> Incomes { get; set; } = new List<Income>();
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public ICollection<AmortizedLoan> AmortizedLoans { get; set; } = new List<AmortizedLoan>();
}
