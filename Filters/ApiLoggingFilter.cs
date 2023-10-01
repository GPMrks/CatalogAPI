using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogAPI.Filters;

public class ApiLoggingFilter : IActionFilter
{

    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("### Executing -> OnActionExecuting");
        _logger.LogInformation("##################################");
        _logger.LogInformation("{LongTimeString}", DateTime.Now.ToLongTimeString());
        _logger.LogInformation("ModelState: {ModelStateIsValid}", context.ModelState.IsValid);
        _logger.LogInformation("##################################");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("### Executing -> OnActionExecuted");
        _logger.LogInformation("##################################");
        _logger.LogInformation("{LongTimeString}", DateTime.Now.ToLongTimeString());
        _logger.LogInformation("##################################");
    }
}