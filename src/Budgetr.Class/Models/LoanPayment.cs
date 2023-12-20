namespace Budgetr.Class.Models;

public readonly struct LoanPayment
{
    public double Interest { get; init; }
    public double Principal { get; init; }
    public double Total => Interest + Principal;

    public static LoanPayment Zero() => new() { Interest = 0, Principal = 0 };
}