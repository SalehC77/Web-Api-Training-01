using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebApplication2.Filters
{
    public class LogActivityFilter : IActionFilter,IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            this._logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger.LogInformation($"Execting action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogInformation($" Action {context.ActionDescriptor.DisplayName} finished execution on controller {context.Controller}");
            context.Result = new NotFoundResult();
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            this._logger.LogInformation($"(Async) Execting action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            this._logger.LogInformation($"(Async) Execting action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
            


        }
    }
}
