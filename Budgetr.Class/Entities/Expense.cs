﻿namespace Budgetr.Class.Entities;

public record Expense
(
    [JsonProperty(PropertyName = "name")]
    string Name,
    [JsonProperty(PropertyName = "amount")]
    double Amount,
    [JsonProperty(PropertyName = "frequency")]
    Frequency Frequency,
    [JsonProperty(PropertyName = "expenseType")]
    ExpenseType ExpenseType
);