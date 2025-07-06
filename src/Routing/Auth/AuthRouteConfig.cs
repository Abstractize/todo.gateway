using Yarp.ReverseProxy.Configuration;

namespace Routing.Auth;

public static class AuthRouteConfig
{
    public static RouteConfig[] GetRoutes() => [
        new RouteConfig
            {
                RouteId = "auth-login",
                ClusterId = "auth-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/auth/login"
                },
                AuthorizationPolicy = "AllowAnonymous",
            },
            new RouteConfig
            {
                RouteId = "auth-refresh",
                ClusterId = "auth-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/auth/refresh-token"
                },
                AuthorizationPolicy = "AllowAnonymous",
            },
            new RouteConfig
            {
                RouteId = "auth-logout",
                ClusterId = "auth-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/auth/logout"
                },
                AuthorizationPolicy = "Authenticated",
            },
            new RouteConfig
            {
                RouteId = "register",
                ClusterId = "auth-cluster",
                Match = new RouteMatch
                {
                    Path = "/api/auth/register"
                },
                AuthorizationPolicy = "AllowAnonymous",
            },
    ];
}