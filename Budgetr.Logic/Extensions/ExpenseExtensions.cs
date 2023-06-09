﻿using Budgetr.Class.Entities;
using Budgetr.Class.Enums;

namespace Budgetr.Logic.Extensions;

public static class ExpenseExtensions
{
    public static Expense As(this Expense expense, Frequency frequency)
    {
        return expense with
        {
            Amount = expense.Amount * (int)expense.Frequency / (int)frequency,
            Frequency = frequency
        };
    }
}