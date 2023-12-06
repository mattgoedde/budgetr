BEGIN TRANSACTION;
GO

ALTER TABLE [AmortizedLoans] ADD [LoanType] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230917182249_EnumConversions', N'8.0.0');
GO

COMMIT;
GO

