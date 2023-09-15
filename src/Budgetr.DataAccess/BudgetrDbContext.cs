using Budgetr.Class.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Budgetr.DataAccess;

public class BudgetrDbContext : DbContext
{
    private readonly ILogger<BudgetrDbContext>? _logger;

    public BudgetrDbContext(DbContextOptions<BudgetrDbContext> options, ILogger<BudgetrDbContext>? logger)
        : base(options)
    {
        _logger = logger;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Budget>().ToTable("Budgets");
        // modelBuilder.Entity<Income>().ToTable("Incomes");
        // modelBuilder.Entity<Deduction>().ToTable("Deductions");
        // modelBuilder.Entity<Expense>().ToTable("Expenses");
        // modelBuilder.Entity<AmortizedLoan>().ToTable("AmortizedLoans");

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
