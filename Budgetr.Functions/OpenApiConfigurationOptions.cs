using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;

public class MyOpenApiConfigurationOptions : IOpenApiConfigurationOptions
{
    public OpenApiInfo Info { get; set; } = new OpenApiInfo()
    {
        Version = "1.0.0",
        Title = "Budgetr API - Azure Functions",
        Description = "HTTP APIs that run on Azure Functions using OpenAPI specification.",
        Contact = new OpenApiContact()
        {
            Name = "Matt",
            Email = "mattgoedde99@gmail.com",
            Url = new Uri("https://github.com/Budgetr/Budgetr/issues"),
        },
        License = new OpenApiLicense(),
    };

    public List<OpenApiServer> Servers { get; set; } = new List<OpenApiServer>();

    public OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V2;
    public bool IncludeRequestingHostName { get; set; } = false;
    public bool ForceHttp { get; set; } = false;
    public bool ForceHttps { get; set; } = false;
}