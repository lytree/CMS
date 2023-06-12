namespace CMS.Web.Aop.User
{
	public class CurrentUserAccessor
	{
		private sealed class CurrentHolder
		{
			public CurrentUser? Context;
		}
		private static AsyncLocal<CurrentHolder> _currentUser { get; set; } = new AsyncLocal<CurrentHolder>();

		/// <summary>
		/// 当前用户信息上下文
		/// </summary>
		/// <value></value>
		public CurrentUser? CurrentUser
		{
			get => _currentUser.Value?.Context;
			set
			{
				var holder = _currentUser.Value;
				if (holder != null)
				{
					// Clear current ICurrentUser trapped in the AsyncLocals, as its done.
					holder.Context = null;
				}

				if (value != null)
				{
					// Use an object indirection to hold the ICurrentUser in the AsyncLocal,
					// so it can be cleared in all ExecutionContexts when its cleared.
					_currentUser.Value = new CurrentHolder { Context = value };
				}
			}
		}
	}
}
