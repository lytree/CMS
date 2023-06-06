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
	public sealed class TagRepository : BaseRepository<Tag, int>
	{
		private readonly ILogger<TagRepository> _logger;
		public TagRepository(ILoggerFactory loggerFactory, IFreeSql fsql, Expression<Func<Tag, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<TagRepository>.Instance : loggerFactory.CreateLogger<TagRepository>();
		}
	}
}
