using Budgetr.Class.Enums;

namespace Budgetr.Class.Models;

public record BudgetSummaryModel
{
    public Guid BudgetId { get; init; }
    public string? BudgetName { get; init; }
    public Frequency Frequency { get; init; }
    public double GrossIncome { get; init; }
    public double PreTaxDeductions { get; init; }
    public double TaxDeductions { get; init; }
    public double PostTaxDeductions { get; init; }
    public double NetIncome => GrossIncome - PreTaxDeductions - TaxDeductions - PostTaxDeductions;
    public double MortagePayment { get; init; }
    public double CarPayment { get; init; }
    public double CreditCardPayment { get; init; }
    public double PersonalLoanPayment { get; init; }
    public double OtherLoanPayment { get; init; }
    public double LoanPayments => MortagePayment + CarPayment + CreditCardPayment + PersonalLoanPayment + OtherLoanPayment;
    public double HousingExpenses { get; init; }
    public double TransportationExpenses { get; init; }
    public double CellularExpenses { get; init; }
    public double GroceryExpenses { get; init; }
    public double LeisureExpenses { get; init; }
    public double MiscellaneousExpenses { get; init; }
    public double Expenses => HousingExpenses + TransportationExpenses + CellularExpenses + GroceryExpenses + LeisureExpenses + MiscellaneousExpenses;
    public double DisposableIncome => NetIncome - LoanPayments - Expenses;
}
