using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using PomodoroApp;
using PomodoroApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TimerSettingsService>();
builder.Services.AddScoped<PomodoroTimerService>();
builder.Services.AddScoped<SoundService>();
builder.Services.AddScoped<BackgroundSettingsService>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddBlazorBootstrap();
await builder.Build().RunAsync();
