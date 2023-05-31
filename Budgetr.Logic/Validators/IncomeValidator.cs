
namespace Budgetr.Logic.Validators;

internal class IncomeValidator : AbstractValidator<Income>
{
    public IncomeValidator()
    {
        RuleFor(i => i.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Income amount cannot be less than 0.");
        RuleFor(i => i.Frequency)
            .IsInEnum()
            .Must(f => f != Frequency.Unknown)
            .WithMessage("Income frequency cannot be unknown.");
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Incomes must have a name.");

        RuleForEach(i => i.Deductions)
            .SetValidator(new DeductionValidator())
            .ChildRules(deductions =>
                deductions.RuleFor(d => d.Frequency)
                    .Must(f => f == Frequency.Weekly)
                    .WithMessage("All Deductions in Income must have same frequency."));

    }
}
