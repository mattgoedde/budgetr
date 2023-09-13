using Microsoft.Extensions.Hosting;

namespace Budgetr.Api;

public class Program
{
    public static void Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .Build();

        host.Run();
    }
}
