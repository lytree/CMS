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
	public sealed class CategoryRepository : BaseRepository<Category, int>
	{
		private readonly ILogger<CategoryRepository> _logger;
		public CategoryRepository(ILoggerFactory loggerFactory,IFreeSql fsql, Expression<Func<Category, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
			_logger = loggerFactory == null ? NullLogger<CategoryRepository>.Instance : loggerFactory.CreateLogger<CategoryRepository>();
		}
	}
}
