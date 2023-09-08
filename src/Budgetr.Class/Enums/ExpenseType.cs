using System.Text.Json.Serialization;

namespace Budgetr.Ui.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
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
