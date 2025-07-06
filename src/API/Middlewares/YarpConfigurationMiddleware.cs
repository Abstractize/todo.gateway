using Routing;

namespace API.Middlewares
{
    internal static class YarpConfigurationMiddleware
    {
        public static IServiceCollection AddReverseProxyConfiguration(this IServiceCollection services)
        {
            services.AddReverseProxyRouting()
                .AddZeroTrustPolicy();

            return services;
        }
    }
}
