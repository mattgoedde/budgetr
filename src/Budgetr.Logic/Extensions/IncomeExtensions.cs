namespace Budgetr.Logic.Extensions;

public static class IncomeExtensions
{
    public static Income To(this Income income, Frequency frequency)
    {
        return income with
        {
            Amount = income.Amount * (int)income.Frequency / (int)frequency,
            Deductions = income.Deductions.Select(d => d.To(frequency)),
            Frequency = frequency,
        };
    }

    public static IEnumerable<Deduction> PreTaxDeductions(this Income income)
    {
        return income.Deductions.Where(d => d.DeductionType == DeductionType.PreTax);
    }

    public static IEnumerable<Deduction> TaxDeductions(this Income income)
    {
        return income.Deductions.Where(d => d.DeductionType == DeductionType.Tax);
    }

    public static IEnumerable<Deduction> PostTaxDeductions(this Income income)
    {
        return income.Deductions.Where(d => d.DeductionType == DeductionType.PostTax);
    }
}
