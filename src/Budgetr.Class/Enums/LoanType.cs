using System.Text.Json.Serialization;

namespace Budgetr.Class.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LoanType
{
    Unknown = 0,
    Mortgage = 1,
    Car = 2,
    Personal = 3,
    CreditCard = 4,
    Other = 5
}