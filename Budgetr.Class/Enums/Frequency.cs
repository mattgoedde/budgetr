using System.Text.Json.Serialization;

namespace Budgetr.Ui.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Frequency
{
    Weekly = 52,
    BiWeekly = 26,
    Monthly = 12,
    Quarterly = 4,
    BiYearly = 2,
    Yearly = 1,
    Unknown = 0
}