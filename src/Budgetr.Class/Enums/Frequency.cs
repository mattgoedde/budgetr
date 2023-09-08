using System.Text.Json.Serialization;

namespace Budgetr.Class.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Frequency
{
    Unknown = 0,
    Yearly = 1,
    BiYearly = 2,
    Quarterly = 4,
    Monthly = 12,
    BiWeekly = 26,
    Weekly = 52,
    Hourly = 2080
}