using Microsoft.AspNetCore.Mvc.Filters;

namespace SimpleDotNetWebApiApp.Filters
{
    public class LogActivityFilter : IActionFilter, IAsyncActionFilter
    {
        readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Before executing action");
            await next();
            _logger.LogInformation("Before executing action");
        }
    }
}
