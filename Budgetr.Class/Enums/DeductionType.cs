namespace Budgetr.Class.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum DeductionType
{
    Unknown,
    PreTax,
    Tax,
    PostTax,
}
