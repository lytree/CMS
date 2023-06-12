using CMS.Web.Aop.User;

namespace CMS.Web.Middleware
{
	public class CurrentUserAccessorMiddleware
	{
		private readonly RequestDelegate _next;
		public CurrentUserAccessorMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		/// <summary>
		/// 通过中间件给当前<see cref="ICurrentUserAccessor"/>赋值<see cref="ICurrentUser"/>
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			var currentAccessor = context.RequestServices.GetService<CurrentUserAccessor>();
			if (currentAccessor != null) currentAccessor.CurrentUser = context.RequestServices.GetRequiredService<CurrentUser>();
			await _next.Invoke(context);
			if (currentAccessor != null) currentAccessor.CurrentUser = null;
		}
	}
}
