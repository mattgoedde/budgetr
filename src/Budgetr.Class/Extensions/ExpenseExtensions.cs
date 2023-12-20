using Budgetr.Class.Entities;
using Budgetr.Class.Enums;

namespace Budgetr.Class.Extensions;

public static class ExpenseExtensions
{
    public static Expense To(this Expense expense, Frequency frequency)
    {
        return expense with
        {
            Amount = expense.Amount * (int)expense.Frequency / (int)frequency,
            Frequency = frequency
        };
    }
}