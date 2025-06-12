namespace SimpleDotNetWebApiApp.Middlewares
{
    public class RateLimitingMiddleware
    {
        readonly ILogger<RateLimitingMiddleware> _logger;
        readonly RequestDelegate _next;
        public static int _counter;
        public static DateTime _lastRequestDate;

        public RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _counter++;
            var now = DateTime.Now;

            if (now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _counter = 1;
                await _next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    await context.Response.WriteAsync("Rate limit exceeded, Please wait 10 second");
                }
                else
                    await _next(context);
            }

            _lastRequestDate = now;
        }
    }
}
