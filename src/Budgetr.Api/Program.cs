using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Budgetr.Api;

public class Program
{
    public static void Main()
    {
        new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            
        })
        .Build()
        .Run();
    }
}
