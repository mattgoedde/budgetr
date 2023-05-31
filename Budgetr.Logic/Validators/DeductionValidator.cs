namespace Budgetr.Logic.Validators;

internal class DeductionValidator : AbstractValidator<Deduction>
{
    public DeductionValidator()
    {
        RuleFor(d => d.Frequency)
            .IsInEnum()
            .Must(f => f != Frequency.Unknown)
            .WithMessage("Deduction frequency cannot be unknown.");
        RuleFor(d => d.DeductionType)
            .IsInEnum()
            .Must(f => f != DeductionType.Unknown)
            .WithMessage("Deduction type cannot be unknown.");
        RuleFor(d => d.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Deduction amount cannot be less than 0.");
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("Deductions must have a name.");
    }
}
