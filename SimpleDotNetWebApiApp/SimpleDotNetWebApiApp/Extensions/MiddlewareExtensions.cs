using SimpleDotNetWebApiApp.Application.Middleware;
using SimpleDotNetWebApiApp.Middlewares;

namespace SimpleDotNetWebApiApp.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseMiddleware<ProfilingMiddleware>();
            app.UseMiddleware<RateLimitingMiddleware>();

            return app;
        }

        public static IApplicationBuilder UseDevelopmentTools(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
