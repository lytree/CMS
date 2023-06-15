using System;
using CMS.Data.Exceptions;
using CMS.Common.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using CMS.Web.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using CMS.Common.Exceptions;

namespace CMS.Web.Aop.Filter;

/// <summary>
/// 出现异常时，如CMSException业务异常，会先执行方法过滤器 （LogActionFilterAttribute）的OnActionExecuted才会执行此异常过滤器。
/// </summary>
public class ControllerExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
{
	private readonly IWebHostEnvironment _env;
	private readonly ILogger<ControllerExceptionFilter> _logger;

	public ControllerExceptionFilter(IWebHostEnvironment env, ILogger<ControllerExceptionFilter> logger)
	{
		_env = env;
		_logger = logger;
	}

	public void OnException(ExceptionContext context)
	{
		if (context.ExceptionHandled == false)
		{
			string message;
			var appException = context.Exception is AppException;
			if (_env.IsProduction())
			{
				message = appException ? context.Exception.Message : CMS.Web.Auth.StatusCodes.Status500InternalServerError.ToDescription();
			}
			else
			{
				message = context.Exception.Message;
			}

			if (!appException)
			{
				_logger.LogError(context.Exception, "");
			}

			var data = ResultOutput.NotOk(message);
			context.Result = new InternalServerErrorResult(data, appException);
		}

		context.ExceptionHandled = true;
	}

	public Task OnExceptionAsync(ExceptionContext context)
	{
		OnException(context);
		return Task.CompletedTask;
	}
}

public class InternalServerErrorResult : ObjectResult
{
	public InternalServerErrorResult(object value, bool appException) : base(value)
	{
		StatusCode = appException ? Microsoft.AspNetCore.Http.StatusCodes.Status200OK : Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
	}
}


