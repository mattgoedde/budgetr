using Budgetr.Class.Entities;

namespace Budgetr.Logic.Validators;

internal class AmortizedLoanValidator : AbstractValidator<AmortizedLoan>
{
    public AmortizedLoanValidator()
    {
        RuleFor(l => l.Name)
            .NotEmpty()
            .WithMessage("Amortized Loans must have a name.");

        RuleFor(l => l.Principal)
            .GreaterThan(0)
            .WithMessage("Amortized Loans must have a loan amount greater than 0.");

        RuleFor(l => l.LoanTermMonths)
            .GreaterThan(0)
            .WithMessage("Amortized Loans must have a term greater than 0 months.");

        RuleFor(l => l.Balance)
            .GreaterThan(0)
            .WithMessage("Amortized Loans must have a remaining balance greater than 0.");

        RuleFor(l => l.AnnualInterestRate)
            .GreaterThan(0)
            .WithMessage("Amortized Loans must have an annual interest rate greater than 0.");
    }
}
