using AutoMapper;
using CMS.Web.Aop.User;
using FreeSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging.Abstractions;

namespace CMS.Web.Service
{
	public abstract class ApplicationService
	{
		public required IServiceProvider ServiceProvider { get; set; }

		protected readonly object ServiceProviderLock = new();

		protected TService LazyGetRequiredService<TService>(ref TService reference)
		{
			return LazyGetRequiredService(typeof(TService), ref reference);
		}

		protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
		{
			if (reference == null)
			{
				lock (ServiceProviderLock)
				{
					if (reference == null)
					{
						reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
					}
				}
			}
			return reference;
		}
		private CurrentUser _currentUser;
		public CurrentUser CurrentUser => LazyGetRequiredService(ref _currentUser);


		private IMapper _mapper;
		public IMapper Mapper => LazyGetRequiredService(ref _mapper);


		private UnitOfWorkManager unitOfWorkManager;
		public UnitOfWorkManager UnitOfWorkManager => LazyGetRequiredService(ref unitOfWorkManager);

		public ILoggerFactory LoggerFactory => LazyGetRequiredService(ref _loggerFactory);
		private ILoggerFactory _loggerFactory;

		protected ILogger Logger => LazyLogger.Value;
		private Lazy<ILogger> LazyLogger => new(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

		public IAuthorizationService AuthorizationService => LazyGetRequiredService(ref _authorizationService);
		private IAuthorizationService _authorizationService;
	}
}
