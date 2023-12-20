using Microsoft.FluentUI.AspNetCore.Components;
using Budgetr.Server.Components;
using Microsoft.EntityFrameworkCore;
using Budgetr.DataAccess;
using Budgetr.Logic.Extensions;
using Microsoft.Identity.Web.UI;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<BudgetrDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Budgetr"));
    })
    .AddBudgetrValidators()
    .AddRazorComponents().AddInteractiveServerComponents().Services
    .AddFluentUIComponents()
    .AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(builder.Configuration["DownstreamApi:Scopes"]?.Split(' '))
    .AddDownstreamWebApi("GraphApi", builder.Configuration.GetSection("GraphApi"))
    .AddInMemoryTokenCaches().Services
    .AddControllersWithViews()
    .AddMicrosoftIdentityUI().Services
    .AddLocalization();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(options =>
{
    options.SetDefaultCulture("en-US");
});

app.UseRouting();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
