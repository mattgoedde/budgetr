using System;
using Budgetr.DataAccess;
using Budgetr.Logic.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Budgetr.Api;

public class Program
{
    public static void Main()
    {
        new HostBuilder().ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            services.AddDbContext<BudgetrDbContext>(options =>
            {
               options.UseSqlServer(Environment.GetEnvironmentVariable("BUDGETR_CONNECTION_STRING")); 
            });
            services.AddBudgetrValidators();
        })
        .Build()
        .Run();
    }
}
