using CMS.Model.Entity;
using FreeSql;
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
		public TagRepository(IFreeSql fsql, Expression<Func<Tag, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
		}
	}
}
