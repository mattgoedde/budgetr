using Budgetr.Class.Enums;
using System.ComponentModel.DataAnnotations;

namespace Budgetr.Class.Entities;

public interface IExpense
{
    string Name { get; }
    double Amount { get; }
    Frequency Frequency { get; }
    ExpenseType ExpenseType { get; }
}

public record Expense : BudgetEntity, IExpense
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Expenses must have a name!")]
    public string Name { get; set; } = string.Empty;
    
    [Range(0.01, double.MaxValue, ErrorMessage = "An expense must be positive")]
    public double Amount { get; set; }
    
    [Range((int)Frequency.Yearly, (int)Frequency.Hourly)]
    public Frequency Frequency { get; set; }

    [Range((int)ExpenseType.Transportation, (int)ExpenseType.Miscellaneous)]
    public ExpenseType ExpenseType { get; set; }
}