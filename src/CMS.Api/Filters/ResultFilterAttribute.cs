using CMS.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection;

namespace CMS.Api.Filters;

public class ResultFilterAttribute : ActionFilterAttribute
{

	/// <summary>
	/// 日志
	/// </summary>
	private readonly ILogger<ResultFilterAttribute> _logger;

	public ResultFilterAttribute(ILoggerFactory? loggerFactory)
	{
		_logger = loggerFactory == null ? NullLogger<ResultFilterAttribute>.Instance : loggerFactory.CreateLogger<ResultFilterAttribute>();
	}
	public override void OnActionExecuted(ActionExecutedContext context)
	{
		if (context.Result is FileResult)
		{
			return;
		}
		if (context.Result is ObjectResult rst)
		{
			object? rstValue = rst?.Value;
			if (context.Exception != null)
			{
				// 异常处理
				context.ExceptionHandled = true;
				if (context.Exception is BusinessException)
				{
					// 如果是用户异常
					context.HttpContext.Response.StatusCode = 200;
					context.Result = new JsonResult(ResultResponse<object>.Fail(((BusinessException)context.Exception).BusinessCode));
					_logger.LogError(context.Exception, context.Exception.Message, ((BusinessException)context.Exception).BusinessCode.GetCode());
				}
				else if (context.Exception is ServerException) { }
				{
					// 如果是系统异常，禁止返回异常的详细信息
					context.HttpContext.Response.StatusCode = 500;
					context.Result = new ContentResult() { Content = ((ServerException)context.Exception).ServerCode.GetMessage() };

					_logger.LogError(context.Exception, context.Exception.Message, ((ServerException)context.Exception).ServerCode.GetCode());

				}
			}
			else
			{
				if (rstValue is ResultResponse<object>)
				{
					// 无异常
					context.Result = new ObjectResult(rstValue);
				}
				else
				{               // 无异常
					context.Result = new ObjectResult(ResultResponse<object>.Success(rstValue));
				}

			}
		}
	}

	public override void OnActionExecuting(ActionExecutingContext context)
	{

	}


}
