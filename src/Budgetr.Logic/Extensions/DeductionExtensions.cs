using Budgetr.Class.Entities;
using Budgetr.Class.Enums;

namespace Budgetr.Logic.Extensions;

public static class DeductionExtensions
{
    public static Deduction To(this Deduction deduction, Frequency frequency)
    {
        return deduction with
        {
            Frequency = frequency,
            Amount = deduction.Amount * (int)deduction.Frequency / (int)frequency
        };
    }
}
