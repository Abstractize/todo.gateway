using API.Middlewares;
using API.Common.Middlewares;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        string JWT_ISSUER = builder.Configuration.GetValue<string>(nameof(JWT_ISSUER))!;
        string JWT_AUDIENCE = builder.Configuration.GetValue<string>(nameof(JWT_AUDIENCE))!;
        string JWT_KEY = builder.Configuration.GetValue<string>(nameof(JWT_KEY))!;

        builder.Services.AddAuthConfiguration(JWT_ISSUER, JWT_AUDIENCE, JWT_KEY);
        builder.Services.AddReverseProxyConfiguration();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapWhen(context =>
                !context.Request.Path.StartsWithSegments("/api"),
                spaApp =>
                {
                    spaApp.UseSpa(spa =>
                    {
                        spa.UseProxyToSpaDevelopmentServer("http://angular-ui:4200");
                    });
                }
            );
        }

        app.UseRouting();
        app.UseAuth();
        app.MapReverseProxy();

        app.Run();
    }
}