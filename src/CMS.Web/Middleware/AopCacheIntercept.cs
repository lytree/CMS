using Castle.DynamicProxy;

namespace CMS.Web.Middleware;

public class AopCacheIntercept : IInterceptor
{
	private readonly AopCacheAsyncIntercept _asyncInterceptor;

	public AopCacheIntercept(AopCacheAsyncIntercept interceptor)
	{
		_asyncInterceptor = interceptor;
	}

	public void Intercept(IInvocation invocation)
	{
		_asyncInterceptor.ToInterceptor().Intercept(invocation);
	}
}
