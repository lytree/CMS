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
	public sealed class PhotoRepository : BaseRepository<Photo, int>
	{
		private readonly ILogger<PhotoRepository> _logger;
		public PhotoRepository(ILoggerFactory loggerFactory, IFreeSql fsql, Expression<Func<Photo, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<PhotoRepository>.Instance : loggerFactory.CreateLogger<PhotoRepository>();
		}
	}
}
