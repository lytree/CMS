using CMS.Model.Entity;
using FreeSql;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Repository
{
	public sealed class UserRepository : BaseRepository<User, int>
	{
		private readonly ILogger<UserRepository> _logger;

		public UserRepository(ILoggerFactory loggerFactory,IFreeSql fsql, Expression<Func<User, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<UserRepository>.Instance : loggerFactory.CreateLogger<UserRepository>();
		}
	}
}
