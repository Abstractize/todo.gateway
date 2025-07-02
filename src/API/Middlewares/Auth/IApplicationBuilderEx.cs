namespace API.Middlewares.Auth
{
    public static class IApplicationBuilderEx
    {
        public static void UseAuthenticationAndAuthorization(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<RoleAuthorizationMiddleware>();
        }
    }
}
