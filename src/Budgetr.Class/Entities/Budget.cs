using System.Text.Json.Serialization;

namespace Budgetr.Class.Entities;

public record Budget : BaseEntity
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("incomes")]
    public ICollection<Income> Incomes { get; set; } = new List<Income>();
    [JsonPropertyName("expenses")]
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    [JsonPropertyName("amortizedLoans")]
    public ICollection<AmortizedLoan> AmortizedLoans { get; set; } = new List<AmortizedLoan>();
}
