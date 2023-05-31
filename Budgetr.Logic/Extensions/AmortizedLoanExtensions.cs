namespace Budgetr.Logic.Extensions;

public static class AmortizedLoanExtensions
{
    public static int TotalPayments(this AmortizedLoan loan) => loan.LoanTermYears * 12;

    private static double TotalMonthlyPayment(double loanAmount, int numberOfPayments, double annualInterestRate, Frequency paymentPeriod = Frequency.Monthly)
    {
        if (loanAmount < 0) throw new ArgumentException("Loan Amount must be positive");
        if (numberOfPayments < 0) throw new ArgumentException("Number of Payments must be positive");
        if (annualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");

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
        if (totalMonthlyPayment < 0) throw new ArgumentException("Total Monthly Payment must be positive");
        if (outstandingLoanBalance < 0) throw new ArgumentException("Outstanding Loan Balance must be positive");
        if (annualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");

        double monthlyInterestRate = annualInterestRate / (int)paymentPeriod;
        double interestPayment = outstandingLoanBalance * monthlyInterestRate;
        double principalPayment = totalMonthlyPayment - interestPayment;
        return principalPayment;
    }

    public static LoanPayment NextPayment(this AmortizedLoan loan)
    {
        if (loan.LoanAmount < 0) throw new ArgumentException("Loan Amount must be positive");
        if (loan.RemainingBalance < 0) throw new ArgumentException("Remaining Balance must be positive");
        if (loan.AnnualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");
        if (loan.TotalPayments() < 0) throw new ArgumentException("Total Payments must be positive");
        
        double totalPayment = TotalMonthlyPayment(loan.LoanAmount, loan.TotalPayments(), loan.AnnualInterestRate);
        double principalPayment = PrincipalPayment(totalPayment, loan.RemainingBalance, loan.AnnualInterestRate);
        
        return new LoanPayment
            (
                Interest: totalPayment - principalPayment,
                Principal: principalPayment
            );
    }

    public static IEnumerable<LoanPayment> RemainingPayments(AmortizedLoan amortizedLoan)
    {
        if (amortizedLoan is null) return Enumerable.Empty<LoanPayment>();

        if (amortizedLoan.LoanAmount < 0) throw new ArgumentException("Loan Amount must be positive");
        if (amortizedLoan.RemainingBalance < 0) throw new ArgumentException("Remaining Balance must be positive");
        if (amortizedLoan.AnnualInterestRate < 0) throw new ArgumentException("Annual Interest Rate must be positive");
        if (amortizedLoan.TotalPayments() < 0) throw new ArgumentException("Total Payments must be positive");

        //double totalPayment = TotalMonthlyPayment(amortizedLoan.LoanAmount, amortizedLoan.TotalPayments, amortizedLoan.AnnualInterestRate);

        double remainingBalance = amortizedLoan.RemainingBalance;
        var payments = new List<LoanPayment>();

        while (remainingBalance > 0)
        {
            var nextPayment = amortizedLoan.NextPayment();
            if (nextPayment.Principal > remainingBalance)
                amortizedLoan = amortizedLoan with { RemainingBalance = 0 };
            else
                amortizedLoan = amortizedLoan with { RemainingBalance = amortizedLoan.RemainingBalance - nextPayment.Principal };
                
            payments.Add(nextPayment);
        }

        return payments;
    }
}
