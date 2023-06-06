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
	public sealed class ConfigRepository : BaseRepository<ConfigGroup, int>
	{
		private readonly ILogger<ConfigRepository> _logger;
		public ConfigRepository(ILoggerFactory loggerFactory,IFreeSql fsql, Expression<Func<ConfigGroup, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<ConfigRepository>.Instance : loggerFactory.CreateLogger<ConfigRepository>();
		}
	}
}
