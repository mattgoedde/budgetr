namespace Budgetr.Class.Entities;

public struct LoanPayment
{
    public double Interest;
    public double Principal;
    public readonly double Total => Interest + Principal;
    public DateTime Period;

    public static LoanPayment Zero() => new() { Interest = 0, Principal = 0, Period = DateTime.Today };
}