﻿using System.Text.Json.Serialization;

namespace Budgetr.Class.Entities;

public interface IEntity
{
    Guid Id { get; }
}

public abstract record BaseEntity : IEntity
{
    public Guid Id { get; set; }
}

public abstract record BudgetEntity : BaseEntity
{
    public Guid BudgetId { get; set; }
    public Budget Budget { get; set; } = null!;
}
