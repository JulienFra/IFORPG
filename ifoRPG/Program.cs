using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ifoRPG;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// --- SECTION POO : Injection de vos services ---
// Vous pouvez ajouter ici un service de gestion d'état pour partager 
// builder.Services.AddSingleton<GameStateService>();

await builder.Build().RunAsync();