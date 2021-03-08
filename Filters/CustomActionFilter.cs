using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SampleAPI.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        private readonly ILogger _logger;
        public CustomActionFilter(ILogger logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Logging OnActionExecuting");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Logging OnActionExecuted");
        }
    }
}
