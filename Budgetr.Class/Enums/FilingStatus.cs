namespace Budgetr.Class.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum FilingStatus
{
    Unknown = 0,
    Single = 1,
    Married = 2
}
