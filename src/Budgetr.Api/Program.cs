using Microsoft.Extensions.Hosting;

namespace Budgetr.Api;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication()
            .Build();

        host.Run();
    }
}
