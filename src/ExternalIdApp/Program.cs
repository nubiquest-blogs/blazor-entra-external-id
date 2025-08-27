using ExternalIdApp.Components;
using ExternalIdApp.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace ExternalIdApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers().AddMicrosoftIdentityUI();

        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddMicrosoftIdentityConsentHandler();
        
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches();

        builder.Services
            .AddHttpClient<ClientService>(client => client.BaseAddress = new Uri("https://backend"))
            .AddMicrosoftIdentityUserAuthenticationHandler("auth", options =>
            {
                options.Scopes = "api://a081ad7b-e67b-41cf-9bbf-991af8a7d0ed/.default" ;
                
            });
        
        builder.Services
            .AddHttpClient<ClientPublicService>(client => client.BaseAddress = new Uri("https://backend"))
            .AddMicrosoftIdentityAppAuthenticationHandler("auth-public", options =>
            {
                options.Scopes = "api://a081ad7b-e67b-41cf-9bbf-991af8a7d0ed/.default" ;
                //options.Scopes = "api://a081ad7b-e67b-41cf-9bbf-991af8a7d0ed/Data:Read";

            });

        builder.Services.AddCascadingAuthenticationState();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapControllers();
        
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}