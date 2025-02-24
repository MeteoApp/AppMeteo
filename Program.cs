using AppMeteo.Components;
using AppMeteo.Models.Internals;
using AppMeteo.Services.Datalayers;
using AppMeteo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped<IRoomDatalayer, RoomDatalayer>();
builder.Services.AddScoped<IMeasuresDatalayer, MeasuresDatalayer>();

builder.Services.Configure<AppMeteo.Models.Internals.Config>(builder.Configuration.GetSection("Config"));

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

await app.RunAsync();
