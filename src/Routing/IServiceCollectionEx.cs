using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace Routing;

public static class IServiceCollectionEx
{
    public static IReverseProxyBuilder AddReverseProxyRouting(this IServiceCollection services)
    {
        RouteConfig[] routes = [
            .. Analytics.AnalyticsRouteConfig.GetRoutes(),
            .. Auth.AuthRouteConfig.GetRoutes(),
            .. Recomendations.RecommendationsRouteConfig.GetRoutes(),
            .. Tasks.TaskRouteConfig.GetRoutes()
        ];

        ClusterConfig[] clusters = [
            .. Analytics.RecommendationsClusterConfig.GetClusters(),
            .. Auth.AuthClusterConfig.GetClusters(),
            .. Recomendations.RecommendationsClusterConfig.GetClusters(),
            .. Tasks.TaskClusterConfig.GetClusters()
        ];

        return services.AddReverseProxy()
            .LoadFromMemory(routes, clusters);
    }

    public static IReverseProxyBuilder AddZeroTrustPolicy(this IReverseProxyBuilder services)
    {
        return services.AddTransforms(transforms =>
        {
            transforms.CopyRequestHeaders = true;
        });
    }
}