using Budgetr.Class.Entities;
using Budgetr.Class.Enums;
using Budgetr.Class.Extensions;

namespace Budgetr.Class.Extensions;

public static class BudgetExtensions
{
    public static Budget To(this Budget input, Frequency newFrequency)
    {
        return input with
        {
            Incomes = input.Incomes?.Select(i => i.To(newFrequency)).ToList(),
            Expenses = input.Expenses?.Select(i => i.To(newFrequency)).ToList()
        };
    }
}
