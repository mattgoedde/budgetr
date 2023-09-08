using System.Text.Json.Serialization;

namespace Budgetr.Ui.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilingStatus
{
    Unknown = 0,
    Single = 1,
    Married = 2
}
