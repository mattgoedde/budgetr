using Budgetr.Class.Entities;

namespace Budgetr.Logic.Extensions;

public static class LoanPaymentExtensions
{
    public static double TotalPayment(this LoanPayment loan) => loan.Principal + loan.Interest;
}
