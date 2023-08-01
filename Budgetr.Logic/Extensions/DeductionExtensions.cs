namespace Budgetr.Logic.Extensions;

public static class DeductionExtensions
{
    public static Deduction As(this Deduction deduction, Frequency frequency)
    {
        return deduction with
        {
            Frequency = frequency,
            Amount = deduction.Amount * (int)deduction.Frequency / (int)frequency
        };
    }
}
