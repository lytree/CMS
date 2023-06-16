using CMS.Data.Attributes;
using CMS.Web.Aop.Logs;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;


namespace CMS.Web.Aop.Filter;

/// <summary>
/// 控制器操作日志记录
/// </summary>
public class ControllerLogFilter : IAsyncActionFilter
{
    private readonly ILogHandler _logHandler;

    public ControllerLogFilter(ILogHandler logHandler)
    {
        _logHandler = logHandler;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(NoOprationLogAttribute)))
        {
            await next();
            return;
        }

        await _logHandler.LogAsync(context, next);
    }
}