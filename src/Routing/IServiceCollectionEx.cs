using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace Routing;

public static class IServiceCollectionEx
{
    public static IReverseProxyBuilder AddReverseProxyRouting(this IServiceCollection services)
    {
        RouteConfig[] routes = [
            .. Auth.AuthRouteConfig.GetRoutes(),
            .. Tasks.TaskRouteConfig.GetRoutes()
        ];

        ClusterConfig[] clusters = [
            .. Auth.AuthClusterConfig.GetClusters(),
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