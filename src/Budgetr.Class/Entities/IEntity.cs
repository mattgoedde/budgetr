using System.Text.Json.Serialization;

namespace Budgetr.Class.Entities;

public interface IEntity
{
    Guid Id { get; }
}

public abstract record BaseEntity : IEntity
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}

public abstract record BudgetEntity : BaseEntity
{
    [JsonPropertyName("budgetId")]
    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; } = null!;
}
