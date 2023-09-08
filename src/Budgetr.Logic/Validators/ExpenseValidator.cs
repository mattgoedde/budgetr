using Budgetr.Class.Entities;
using Budgetr.Class.Enums;

namespace Budgetr.Logic.Validators;

internal class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Expenses must have a name.");

        RuleFor(e => e.Frequency)
            .IsInEnum()
            .Must(f => f != Frequency.Unknown)
            .WithMessage("Expense frequency cannot be unknown.");

        RuleFor(e => e.ExpenseType)
            .IsInEnum()
            .Must(et => et != ExpenseType.Unknown)
            .WithMessage("Expense type cannot be unknown.");

        RuleFor(e => e.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Expense amount cannot be less than 0.");
    }
}
