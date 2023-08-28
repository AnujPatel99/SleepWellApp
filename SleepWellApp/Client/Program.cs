using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SleepWellApp.Client;
using MudBlazor.Services;
using OpenAI.Managers;
using OpenAI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var openAiService = new OpenAIService(new OpenAiOptions()
{
    ApiKey = Environment.GetEnvironmentVariable("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")
});

builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SleepWellApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SleepWellApp.ServerAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
