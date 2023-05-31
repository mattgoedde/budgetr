namespace Budgetr.Logic.Validators;

internal class BudgetValidator : AbstractValidator<Budget>
{
    public BudgetValidator()
    {
        RuleForEach(b => b.Comment)
            .NotEmpty()
            .WithMessage("Budgets must have a comment.");

        RuleForEach(b => b.Expenses).SetValidator(new ExpenseValidator());
        RuleForEach(b => b.Incomes).SetValidator(new IncomeValidator());
        RuleForEach(b => b.OtherLoans).SetValidator(new AmortizedLoanValidator());
        RuleForEach(b => b.AutoLoans).SetValidator(new AmortizedLoanValidator());
        RuleForEach(b => b.HousingLoans).SetValidator(new AmortizedLoanValidator());
    }
}
