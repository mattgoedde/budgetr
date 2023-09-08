using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Budgetr.DataAccess;

public class BudgetrDbContextFactory : IDesignTimeDbContextFactory<BudgetrDbContext>
{
    public BudgetrDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BudgetrDbContext>();
        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Budgetr;Trusted_Connection=True;");

        return new BudgetrDbContext(options.Options, null);
    }
}
