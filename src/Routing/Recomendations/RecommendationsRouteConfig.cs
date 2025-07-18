using Yarp.ReverseProxy.Configuration;

namespace Routing.Recomendations;

public static class RecommendationsRouteConfig
{
    public static RouteConfig[] GetRoutes() => [
        new RouteConfig
        {
            RouteId = "recommendations",
            ClusterId = "recommendations-cluster",
            Match = new RouteMatch
            {
                Path = "/api/recommendations/{**catch-all}"
            },
            Transforms = new List<Dictionary<string, string>>
            {
                new() { { "PathRemovePrefix", "/api/recommendations" } }
            },
            AuthorizationPolicy = "Authenticated",
        }
    ];
}