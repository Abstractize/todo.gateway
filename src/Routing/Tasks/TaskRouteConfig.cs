using Yarp.ReverseProxy.Configuration;

namespace Routing.Tasks;

public static class TaskRouteConfig
{
    public static RouteConfig[] GetRoutes() => [
        new RouteConfig
        {
            RouteId = "tasks",
            ClusterId = "task-cluster",
            Match = new RouteMatch
            {
                Path = "/api/tasks/{**catch-all}"
            },
            Transforms = new List<Dictionary<string, string>>
            {
                new() { { "PathRemovePrefix", "/api/tasks" } },
                new() { { "PathPrefix", "/api" } }
            },
            AuthorizationPolicy = "Authenticated",
        }
    ];
}