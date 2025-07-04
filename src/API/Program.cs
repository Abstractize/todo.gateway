using API.Middlewares;
using API.Middlewares.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string JWT_ISSUER = builder.Configuration.GetValue<string>(nameof(JWT_ISSUER))!;
        string JWT_AUDIENCE = builder.Configuration.GetValue<string>(nameof(JWT_AUDIENCE))!;
        string JWT_KEY = builder.Configuration.GetValue<string>(nameof(JWT_KEY))!;

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JWT_ISSUER,
                    ValidAudience = JWT_AUDIENCE,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JWT_KEY))
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddReverseProxyConfiguration();

        builder.Services.AddOpenApi();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseAuthenticationAndAuthorization();

        app.MapWhen(context => context.Request.Path.StartsWithSegments("/api"), apiApp =>
        {
            apiApp.UseRouting();
            apiApp.UseEndpoints(endpoints => endpoints.MapReverseProxy());
        });


        if (app.Environment.IsDevelopment())
        {
            app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"), spaApp =>
            {
                spaApp.UseSpa(spa =>
                {
                    spa.UseProxyToSpaDevelopmentServer("http://angular-ui:4200");
                });
            });
        }

        app.Run();
    }
}