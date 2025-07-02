using Yarp.ReverseProxy.Configuration;

namespace API.Middlewares.Auth
{
    internal class RoleAuthorizationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.GetEndpoint();
            var yarpRoute = endpoint?.Metadata.GetMetadata<RouteConfig>();

            if (yarpRoute != null &&
                yarpRoute.Metadata.TryGetValue("RequiredRole", out var userRole) &&
                Enum.TryParse<UserRoles>(userRole, out var requiredRole))
            {
                var user = context.User;
                if (!user.Identity?.IsAuthenticated ?? true)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                if (requiredRole == UserRoles.User && !user.IsInRole(nameof(UserRoles.User)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
                if (requiredRole == UserRoles.Administator && !user.IsInRole(nameof(UserRoles.Administator)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await next(context);
        }
    }
}
