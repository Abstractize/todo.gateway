using Yarp.ReverseProxy.Configuration;

namespace Routing.Analytics;

public static class AnalyticsRouteConfig
{
    public static RouteConfig[] GetRoutes() => [
        new RouteConfig
        {
            RouteId = "analytics",
            ClusterId = "analytics-cluster",
            Match = new RouteMatch
            {
                Path = "/api/analytics/{**catch-all}"
            },
            Transforms = new List<Dictionary<string, string>>
            {
                new() { { "PathRemovePrefix", "/api/analytics" } }
            },
            AuthorizationPolicy = "Authenticated",
        }
    ];
}