namespace WebApi.Middleware
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IServiceCollection AddErrorHandlingMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<ErrorHandlingMiddleware>();
        }

        public static void UseErrorHandlingMiddlware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
