using Budgetr.Class.Entities;
using Budgetr.Class.Enums;
using Budgetr.Class.Extensions;

namespace Budgetr.Class.Extensions;

public static class IncomeExtensions
{
    public static Income To(this Income income, Frequency frequency)
    {
        return income with
        {
            Amount = income.Amount * (int)income.Frequency / (int)frequency,
            Deductions = income.Deductions?.Select(d => d.To(frequency)).ToList(),
            Frequency = frequency,
        };
    }
}
