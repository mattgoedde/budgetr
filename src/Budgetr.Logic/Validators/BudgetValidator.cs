namespace Budgetr.Logic.Validators;

internal class BudgetValidator : AbstractValidator<Budget>
{
    public BudgetValidator()
    {
        RuleFor(l => l.Name)
            .NotEmpty()
            .WithMessage("Budgets must have a name.");
    }
}
