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
	public sealed class PhotoRepository : BaseRepository<Photo, int>
	{
		public PhotoRepository(IFreeSql fsql, Expression<Func<Photo, bool>>? filter, Func<string, string> asTable = null) : base(fsql, filter, asTable)
		{
		}
	}
}
