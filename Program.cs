using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PomodoroApp;
using PomodoroApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<TimerSettingsService>();
builder.Services.AddScoped<PomodoroTimerService>();
builder.Services.AddBlazorBootstrap();
builder.Services.AddScoped<SoundService>();
await builder.Build().RunAsync();
