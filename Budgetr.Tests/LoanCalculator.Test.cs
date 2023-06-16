namespace Budgetr.Tests;

public class TestLoanCalculator
{
    private AmortizedLoan? _mortgage;
    private AmortizedLoan? _car;

    [SetUp]
    public void Setup()
    {
        _mortgage = new AmortizedLoan
        {
            Name = "Test Mortgage",
            LoanAmount = 500000,
            RemainingBalance = 500000,
            AnnualInterestRate = 0.05,
            LoanTermMonths = 30 * 12
        };

        _car = new AmortizedLoan
        {
            Name = "Test Car Loan",
            LoanAmount = 15142,
            RemainingBalance = 15142,
            AnnualInterestRate = 0.0625,
            LoanTermMonths = 66
        };
    }

    [Test]
    public void Loan_Calculator_Should_Produce_Correct_Values()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_mortgage, Is.Not.Null);
            Assert.That(_car, Is.Not.Null);

            LoanPayment mortgagePayment = LoanPayment.Zero();
            LoanPayment carPayment = LoanPayment.Zero();

            Assert.DoesNotThrow(() =>
            {
                mortgagePayment = _mortgage!.NextPayment();
                carPayment = _car!.NextPayment();
            });

            Assert.That(mortgagePayment.Total, Is.EqualTo(2684.11).Within(0.01));
            Assert.That(mortgagePayment.Interest, Is.EqualTo(2083.33).Within(0.01));
            Assert.That(mortgagePayment.Principal, Is.EqualTo(600.78).Within(0.01));

            Assert.That(carPayment.Total, Is.EqualTo(271.70).Within(0.01));
            Assert.That(carPayment.Interest, Is.EqualTo(78.86).Within(0.01));
            Assert.That(carPayment.Principal, Is.EqualTo(192.84).Within(0.01));
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Interest_Rate()
    {
        var badLoan = new AmortizedLoan
        {
            Name = "",
            LoanAmount = 1000,
            RemainingBalance = 1000,
            AnnualInterestRate = -0.01,
            LoanTermMonths = 5
        };

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Loan_Amount()
    {
        var badLoan = new AmortizedLoan
        {
            Name = "",
            LoanAmount = -1000,
            RemainingBalance = 1000,
            AnnualInterestRate = 0.01,
            LoanTermMonths = 5
        };

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Remaining_Balance()
    {
        var badLoan = new AmortizedLoan
        {
            Name = "",
            LoanAmount = 1000,
            RemainingBalance = -1000,
            AnnualInterestRate = 0.01,
            LoanTermMonths = 5
        };

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }
    
    [Test]
    public void Loan_Calculator_Handles_Bad_Loan_Term()
    {
        var badLoan = new AmortizedLoan
        {
            Name = "",
            LoanAmount = 1000,
            RemainingBalance = 1000,
            AnnualInterestRate = 0.01,
            LoanTermMonths = -5
        };

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }
}