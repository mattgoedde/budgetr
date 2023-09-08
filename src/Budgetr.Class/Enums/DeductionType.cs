using System.Text.Json.Serialization;

namespace Budgetr.Class.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeductionType
{
    Unknown = 0,
    PreTax = 1,
    Tax = 2,
    PostTax = 3
}
