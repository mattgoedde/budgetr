using System.Text.Json.Serialization;

namespace Budgetr.Ui.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeductionType
{
    Unknown,
    PreTax,
    Tax,
    PostTax,
}
