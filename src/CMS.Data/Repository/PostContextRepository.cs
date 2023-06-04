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
	public sealed class PostContextRepository : BaseRepository<PostContext, int>
	{
		private readonly ILogger<PostContextRepository> _logger;

		public PostContextRepository(ILoggerFactory loggerFactory, IFreeSql fsql, Expression<Func<PostContext, bool>>? filter = null, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<PostContextRepository>.Instance : loggerFactory.CreateLogger<PostContextRepository>();
		}
	}
}
