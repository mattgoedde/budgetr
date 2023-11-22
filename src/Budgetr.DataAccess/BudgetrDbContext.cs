using Budgetr.Class.Entities;
using Budgetr.Class.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetr.DataAccess;

public class BudgetrDbContext : DbContext
{
    private readonly ILogger<BudgetrDbContext>? _logger;

    public BudgetrDbContext(DbContextOptions<BudgetrDbContext> options, ILogger<BudgetrDbContext>? logger = null)
        : base(options)
    {
        _logger = logger;
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DeductionType>().HaveConversion<int>();
        configurationBuilder.Properties<ExpenseType>().HaveConversion<int>();
        configurationBuilder.Properties<LoanType>().HaveConversion<int>();
        configurationBuilder.Properties<Frequency>().HaveConversion<int>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Income>()
            .HasMany(i => i.Deductions)
            .WithOne(d => d.Income)
            .OnDelete(DeleteBehavior.ClientCascade);

        modelBuilder
            .Entity<Income>()
            .HasOne(i => i.Budget)
            .WithMany(b => b.Incomes);
        modelBuilder
            .Entity<Expense>()
            .HasOne(e => e.Budget)
            .WithMany(b => b.Expenses);
        modelBuilder
            .Entity<AmortizedLoan>()
            .HasOne(l => l.Budget)
            .WithMany(b => b.AmortizedLoans);
    }

    public DbSet<Budget> Budgets { get; set; } = default!;
    public DbSet<AmortizedLoan> AmortizedLoans { get; set; } = default!;
    public DbSet<Expense> Expenses { get; set; } = default!;
    public DbSet<Deduction> Deductions { get; set; } = default!;
    public DbSet<Income> Incomes { get; set; } = default!;
}
