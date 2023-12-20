using Microsoft.FluentUI.AspNetCore.Components;
using Budgetr.Server.Components;
using Microsoft.EntityFrameworkCore;
using Budgetr.DataAccess;
using Budgetr.Logic.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddDbContext<BudgetrDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Budgetr"));
    })
    .AddBudgetrValidators()
    .AddRazorComponents().AddInteractiveServerComponents().Services
    .AddFluentUIComponents();

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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
