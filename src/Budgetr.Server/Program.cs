using Microsoft.FluentUI.AspNetCore.Components;
using Budgetr.Server.Components;
using Microsoft.EntityFrameworkCore;
using Budgetr.DataAccess;
using Budgetr.Logic.Extensions;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<BudgetrDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Budgetr"));
    })
    .AddBudgetrValidators()
    .AddRazorComponents()
    .AddInteractiveServerComponents().Services
    .AddFluentUIComponents()
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C")).Services
    .AddControllersWithViews(options =>
    {
        var policy = new AuthorizationPolicyBuilder()
                      .RequireAuthenticatedUser()
                      .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }).AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
