namespace Budgetr.Class.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ExpenseType
{
    Transportation,
    Housing,
    Cellular,
    Grocery,
    Leisure,
    Miscellaneous,
    Unknown
}
