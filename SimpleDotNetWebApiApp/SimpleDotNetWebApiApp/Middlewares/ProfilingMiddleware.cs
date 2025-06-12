using System.Diagnostics;

namespace SimpleDotNetWebApiApp.Middlewares
{
    public class ProfilingMiddleware
    {
        readonly ILogger _logger;
        readonly RequestDelegate _next;

        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //context.Response.OnStarting(() =>
            //{
            //    stopwatch.Stop();
            //    _logger.LogInformation($"Request took {stopwatch.ElapsedMilliseconds}ms");
            //    return Task.CompletedTask;
            //});
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request took {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
