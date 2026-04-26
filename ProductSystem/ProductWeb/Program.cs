using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductWeb;
using ProductWeb.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient para archivos locales (como weather.json)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// HttpClient específico para la API
builder.Services.AddHttpClient("ProductApi", client => 
{
    client.BaseAddress = new Uri("http://localhost:5117/");
});

builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
