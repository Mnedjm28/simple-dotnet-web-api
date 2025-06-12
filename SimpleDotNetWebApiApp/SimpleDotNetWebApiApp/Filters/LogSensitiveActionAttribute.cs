using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace SimpleDotNetWebApiApp.Filters
{
    public class LogSensitiveActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action executed");
        }
    }
}
