using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Budgetr.Ui;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.AspNetCore.Components.Authorization;
using Budgetr.Ui.Authentication;
using Budgetr.Ui.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<BudgetrApiService>()
    .AddAuthorizationCore()
    .AddScoped<AuthenticationStateProvider, StaticWebAppAuthenticationStateProvider>();

builder.Services.AddFluentUIComponents(options =>
{
    options.HostingModel = BlazorHostingModel.WebAssembly;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
