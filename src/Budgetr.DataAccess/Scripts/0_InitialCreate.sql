IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Budgets] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Budgets] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AmortizedLoans] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Principal] float NOT NULL,
    [Balance] float NOT NULL,
    [AnnualInterestRate] float NOT NULL,
    [LoanTermMonths] int NOT NULL,
    [BudgetId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AmortizedLoans] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AmortizedLoans_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Expenses] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Amount] float NOT NULL,
    [Frequency] int NOT NULL,
    [ExpenseType] int NOT NULL,
    [BudgetId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Expenses_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Incomes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Amount] float NOT NULL,
    [Frequency] int NOT NULL,
    [BudgetId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Incomes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Incomes_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Deductions] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Amount] float NOT NULL,
    [Frequency] int NOT NULL,
    [DeductionType] int NOT NULL,
    [IncomeId] uniqueidentifier NOT NULL,
    [BudgetId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Deductions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Deductions_Budgets_BudgetId] FOREIGN KEY ([BudgetId]) REFERENCES [Budgets] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Deductions_Incomes_IncomeId] FOREIGN KEY ([IncomeId]) REFERENCES [Incomes] ([Id])
);
GO

CREATE INDEX [IX_AmortizedLoans_BudgetId] ON [AmortizedLoans] ([BudgetId]);
GO

CREATE INDEX [IX_Deductions_BudgetId] ON [Deductions] ([BudgetId]);
GO

CREATE INDEX [IX_Deductions_IncomeId] ON [Deductions] ([IncomeId]);
GO

CREATE INDEX [IX_Expenses_BudgetId] ON [Expenses] ([BudgetId]);
GO

CREATE INDEX [IX_Incomes_BudgetId] ON [Incomes] ([BudgetId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230915182927_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

