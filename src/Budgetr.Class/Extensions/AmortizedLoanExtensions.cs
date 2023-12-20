using Budgetr.Class.Entities;
using Budgetr.Class.Enums;
using Budgetr.Class.Models;

namespace Budgetr.Logic.Extensions;

public static class AmortizedLoanExtensions
{
    private static double TotalMonthlyPayment(double loanAmount, int numberOfPayments, double annualInterestRate, Frequency paymentPeriod = Frequency.Monthly)
    {
        if (double.IsNaN(loanAmount) || loanAmount < 0) throw new ArgumentException("Loan Amount must be positive");
        if (double.IsNaN(numberOfPayments) || numberOfPayments <= 0) throw new ArgumentException("Number of Payments must be greater than 0");
        if (double.IsNaN(annualInterestRate) || annualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");

        if (annualInterestRate == 0)
            return loanAmount / numberOfPayments;

        double monthlyInterestRate = annualInterestRate / (int)paymentPeriod;
        double m = Math.Pow((1 + monthlyInterestRate), numberOfPayments);
        double n = monthlyInterestRate * m;
        double o = m - 1;
        double p = n / o;
        double totalMonthlyPayment = loanAmount * p;
        return totalMonthlyPayment;
    }

    private static double PrincipalPayment(double totalMonthlyPayment, double outstandingLoanBalance, double annualInterestRate, Frequency paymentPeriod = Frequency.Monthly)
    {
        if (double.IsNaN(totalMonthlyPayment) || totalMonthlyPayment < 0) throw new ArgumentException("Total Monthly Payment must be positive");
        if (double.IsNaN(outstandingLoanBalance) || outstandingLoanBalance < 0) throw new ArgumentException("Outstanding Loan Balance must be positive");
        if (double.IsNaN(annualInterestRate) || annualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");

        double monthlyInterestRate = annualInterestRate / (int)paymentPeriod;
        double interestPayment = outstandingLoanBalance * monthlyInterestRate;
        double principalPayment = totalMonthlyPayment - interestPayment;
        return principalPayment;
    }

    public static LoanPayment NextPayment(this AmortizedLoan amortizedLoan)
    {
        if (double.IsNaN(amortizedLoan.Principal) ||
            double.IsNaN(amortizedLoan.Balance) ||
            double.IsNaN(amortizedLoan.AnnualInterestRate) ||
            double.IsNaN(amortizedLoan.LoanTermMonths))
            return LoanPayment.Zero();

        if (amortizedLoan.Principal < 0) throw new ArgumentException("Loan Amount must be positive");
        if (amortizedLoan.Balance < 0) throw new ArgumentException("Remaining Balance must be positive");
        if (amortizedLoan.AnnualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");
        if (amortizedLoan.LoanTermMonths < 0) throw new ArgumentException("Loan Term must be positive");
        
        double totalPayment = TotalMonthlyPayment(amortizedLoan.Principal, amortizedLoan.LoanTermMonths, amortizedLoan.AnnualInterestRate);
        if (double.IsNaN(totalPayment) || totalPayment <= 0) return new LoanPayment();
        double principalPayment = PrincipalPayment(totalPayment, amortizedLoan.Balance, amortizedLoan.AnnualInterestRate);

        return new LoanPayment
        {
            Interest = totalPayment - principalPayment,
            Principal = principalPayment,
        };
    }

    public static IEnumerable<LoanPayment> AllPayments(this AmortizedLoan amortizedLoan)
    {
        if (double.IsNaN(amortizedLoan.Principal) ||
            double.IsNaN(amortizedLoan.Balance) ||
            double.IsNaN(amortizedLoan.AnnualInterestRate) ||
            double.IsNaN(amortizedLoan.LoanTermMonths))
            return Enumerable.Empty<LoanPayment>();

        amortizedLoan = new AmortizedLoan
        {
            Balance = amortizedLoan.Principal,
            Principal = amortizedLoan.Principal,
            Name = amortizedLoan.Name,
            AnnualInterestRate = amortizedLoan.AnnualInterestRate,
            LoanTermMonths = amortizedLoan.LoanTermMonths
        };
        return amortizedLoan.RemainingPayments();
    }

    public static IEnumerable<LoanPayment> RemainingPayments(this AmortizedLoan amortizedLoan)
    {
        if (amortizedLoan is null) 
            yield break;

        if (double.IsNaN(amortizedLoan.Principal) ||
            double.IsNaN(amortizedLoan.Balance) ||
            double.IsNaN(amortizedLoan.AnnualInterestRate) ||
            double.IsNaN(amortizedLoan.LoanTermMonths))
            yield break;

        if (amortizedLoan.Principal == 0) 
            yield break;

        if (amortizedLoan.Principal < 0) throw new ArgumentException("Loan Amount must be positive");
        if (amortizedLoan.Balance < 0) throw new ArgumentException("Remaining Balance must be positive");
        if (amortizedLoan.AnnualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");
        if (amortizedLoan.LoanTermMonths < 0) throw new ArgumentException("Loan Term Months must be positive");

        while (amortizedLoan.Balance > 0)
        {
            var nextPayment = amortizedLoan.NextPayment();

            if (nextPayment.Principal > amortizedLoan.Balance)
            {
                amortizedLoan = new AmortizedLoan
                {
                    Name = amortizedLoan.Name,
                    AnnualInterestRate = amortizedLoan.AnnualInterestRate,
                    LoanTermMonths = amortizedLoan.LoanTermMonths,
                    Principal = amortizedLoan.Principal,
                    Balance = 0
                };
            }
            else
            {
                amortizedLoan = new AmortizedLoan
                {
                    Name = amortizedLoan.Name,
                    AnnualInterestRate = amortizedLoan.AnnualInterestRate,
                    LoanTermMonths = amortizedLoan.LoanTermMonths,
                    Principal = amortizedLoan.Principal,
                    Balance = amortizedLoan.Balance - nextPayment.Principal,
                };
            }
            yield return nextPayment;
        }
    }
}
