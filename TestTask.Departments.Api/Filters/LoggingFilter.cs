using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestTask.Departments.Api.Filters
{
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName;

            _logger.LogInformation($"User {context.HttpContext.User?.Identity?.Name}invoke method {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
