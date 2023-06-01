using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Context
{
	public class PhotoContext : DbContext
	{
		public DbSet<PhotoContext> Photo { get; set; }

		protected override void OnModelCreating(ICodeFirst codeFirst)
		{

		}
	}
}
