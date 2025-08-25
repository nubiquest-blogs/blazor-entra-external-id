using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddAuthentication(OpenIdConnectDefaults
        .AuthenticationScheme) // This means default scheme is "OpenIdConnect"
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"), OpenIdConnectDefaults.AuthenticationScheme);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdmin", policy =>
    {
        policy.RequireRole("role");
        //policy.AddAuthenticationSchemes(OpenIdConnectDefaults.AuthenticationScheme);
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

