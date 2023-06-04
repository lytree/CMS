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
	public sealed class LinkRepository : BaseRepository<Link, int>
	{
		private readonly ILogger<LinkRepository> _logger;

		public LinkRepository(ILoggerFactory loggerFactory,IFreeSql fsql, Expression<Func<Link, bool>> filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<LinkRepository>.Instance : loggerFactory.CreateLogger<LinkRepository>();
		}
	}
}
