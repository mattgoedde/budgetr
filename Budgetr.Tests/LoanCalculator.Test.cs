namespace Budgetr.Tests;

public class TestLoanCalculator
{
    private AmortizedLoan? _mortgage;
    private AmortizedLoan? _car;

    [SetUp]
    public void Setup()
    {
        _mortgage = new AmortizedLoan
        (
            Name: "Test Mortgage",
            LoanAmount: 500000,
            RemainingBalance: 500000,
            AnnualInterestRate: 0.05,
            LoanTermYears: 30
        );

        _car = new AmortizedLoan
        (
            Name: "Test Car Loan",
            LoanAmount: 50000,
            RemainingBalance: 50000,
            AnnualInterestRate: 0.05,
            LoanTermYears: 5
        );
    }

    [Test]
    public void Loan_Calculator_Should_Produce_Correct_Values()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_mortgage, Is.Not.Null);
            Assert.That(_car, Is.Not.Null);

            LoanPayment? mortgagePayment = null;
            LoanPayment? carPayment = null;

            Assert.DoesNotThrow(() =>
            {
                mortgagePayment = _mortgage!.NextPayment();
                carPayment = _car!.NextPayment();
            });

            Assert.That(mortgagePayment, Is.Not.Null);
            Assert.That(carPayment, Is.Not.Null);

            Assert.That(mortgagePayment!.TotalPayment(), Is.EqualTo(2684.11).Within(0.01));
            Assert.That(mortgagePayment!.Interest, Is.EqualTo(2083.33).Within(0.01));
            Assert.That(mortgagePayment!.Principal, Is.EqualTo(600.78).Within(0.01));

            Assert.That(carPayment!.TotalPayment(), Is.EqualTo(943.56).Within(0.01));
            Assert.That(carPayment!.Interest, Is.EqualTo(208.33).Within(0.01));
            Assert.That(carPayment!.Principal, Is.EqualTo(735.23).Within(0.01));
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Interest_Rate()
    {
        var badLoan = new AmortizedLoan
        (
            Name: "",
            LoanAmount: 1000,
            RemainingBalance: 1000,
            AnnualInterestRate: -0.01,
            LoanTermYears: 5
        );

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Loan_Amount()
    {
        var badLoan = new AmortizedLoan
        (
            Name: "",
            LoanAmount: -1000,
            RemainingBalance: 1000,
            AnnualInterestRate: 0.01,
            LoanTermYears: 5
        );

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }

    [Test]
    public void Loan_Calculator_Handles_Bad_Remaining_Balance()
    {
        var badLoan = new AmortizedLoan
        (
            Name: "",
            LoanAmount: 1000,
            RemainingBalance: -1000,
            AnnualInterestRate: 0.01,
            LoanTermYears: 5
        );

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }
    
    [Test]
    public void Loan_Calculator_Handles_Bad_Loan_Term()
    {
        var badLoan = new AmortizedLoan
        (
            Name: "",
            LoanAmount: 1000,
            RemainingBalance: 1000,
            AnnualInterestRate: 0.01,
            LoanTermYears: -5
        );

        Assert.Throws<ArgumentException>(() =>
        {
            badLoan.NextPayment();
        });
    }
}