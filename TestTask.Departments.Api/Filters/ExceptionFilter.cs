using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TestTask.Departments.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var actionName = (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName;

            _logger.LogError($"Exception in method {actionName}. Error: {context.Exception}");
        }
    }
}
