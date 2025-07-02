using API.Middlewares.Auth;
using Microsoft.AspNetCore.Authorization;
using Yarp.ReverseProxy.Configuration;

namespace API.Middlewares
{
    internal static class YarpConfigurationMiddleware
    {
        private static readonly IReadOnlyList<RouteConfig> Routes = [
            new RouteConfig
            {
                RouteId = "auth",
                ClusterId = "auth-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/auth/{**catch-all}"
                },
            },
            new RouteConfig
            {
                RouteId = "task",
                ClusterId = "task-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/tasks/{**catch-all}"
                },
                //AuthorizationPolicy = "Authenticated",
                Metadata = new Dictionary<string, string>
                {
                    ["UserRole"] = nameof(UserRoles.User)
                }
            },
        ];

        private static readonly IReadOnlyList<ClusterConfig> Clusters = [
            new ClusterConfig
            {
                ClusterId = "auth-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "auth-service", new DestinationConfig { Address = "http://auth-service:8080" } }
                }
            },
            new ClusterConfig{
                ClusterId = "task-cluster",
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "task-service", new DestinationConfig { Address = "http://task-service:8080" } }
                }    
            }
        ];

        public static IServiceCollection AddReverseProxyConfiguration(this IServiceCollection services)
        {
            services.AddReverseProxy()
                .LoadFromMemory(Routes, Clusters);

            return services;
        }
    }
}
