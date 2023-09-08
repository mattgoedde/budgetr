namespace Budgetr.Logic.Extensions;

public static class BudgetExtensions
{
    public static double GrossIncome(this Budget budget, Frequency frequency)
        => budget.Incomes?.Select(i => i.As(frequency)).Sum(i => i.Amount) ?? 0;

    public static double NetIncome(this Budget budget, Frequency frequency)
        => budget.GrossIncome(frequency) - budget.TotalDeductions(frequency);

    public static double TotalDeductions(this Budget budget, Frequency frequency)
        => budget.Incomes?.Select(i => i.As(frequency)).Sum(i => i.Deductions?.Sum(d => d.Amount) ?? 0) ?? 0;

    public static double TotalDeductions(this Budget budget, DeductionType deductionType, Frequency frequency)
        => budget.Incomes?.Select(i => i.As(frequency)).Sum(i => i.Deductions?.Where(d => d.DeductionType == deductionType).Sum(d => d.Amount) ?? 0) ?? 0;

    public static double TotalHousing(this Budget budget, Frequency frequency)
        => (budget.HousingLoans?.Sum(l => l.NextPayment().Total) ?? 0) * (int)Frequency.Monthly / (int)frequency + TotalExpenses(budget, ExpenseType.Housing, frequency);

    public static double TotalTransportation(this Budget budget, Frequency frequency)
        => (budget.AutoLoans?.Sum(l => l.NextPayment().Total) ?? 0) * (int)Frequency.Monthly / (int)frequency + TotalExpenses(budget, ExpenseType.Transportation, frequency);

    public static double TotalExpenses(this Budget budget, Frequency frequency)
        => budget.Expenses?.Select(e => e.As(frequency)).Sum(e => e.Amount) ?? 0;

    public static double TotalExpenses(this Budget budget, ExpenseType expenseType, Frequency frequency)
        => budget.Expenses?.Where(e => e.ExpenseType == expenseType).Select(e => e.As(frequency)).Sum(e => e.Amount) ?? 0;

    public static double TotalPreTaxDeductions(this Budget budget, Frequency frequency)
        => budget.TotalDeductions(DeductionType.PreTax, frequency);

    public static double TotalTaxDeductions(this Budget budget, Frequency frequency)
        => budget.TotalDeductions(DeductionType.Tax, frequency);

    public static double TotalPostTaxDeductions(this Budget budget, Frequency frequency)
        => budget.TotalDeductions(DeductionType.PostTax, frequency);

    public static double TotalOtherLoans(this Budget budget, Frequency frequency)
        => (budget.OtherLoans?.Sum(l => l.NextPayment().Total) ?? 0) * (int)Frequency.Monthly / (int)frequency;

    public static double DisposableIncome(this Budget budget, Frequency frequency)
        => budget.GrossIncome(frequency) // gross Income
            - budget.TotalDeductions(frequency) // deductions
            - budget.TotalHousing(frequency) // mortagages and housing costs
            - budget.TotalTransportation(frequency) // auto loans and auto costs
            - budget.TotalOtherLoans(frequency) // other loans
            - budget.TotalExpenses(frequency); // other expenses
}
