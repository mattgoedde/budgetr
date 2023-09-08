using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
