using System.Text.Json.Serialization;

namespace Budgetr.Class.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExpenseType
{
    Unknown = 0,
    Transportation = 1,
    Housing = 2,
    Cellular = 3,
    Grocery = 4,
    Leisure = 5,
    Miscellaneous = 6
}
