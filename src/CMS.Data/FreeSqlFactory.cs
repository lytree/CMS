
using FreeSql;
using FreeSql.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
	public class FreeSqlFactory
	{
		public static IFreeSql Create(DataType dataType, string connectionString)
		{
			IFreeSql fsql = new FreeSql.FreeSqlBuilder()
				.UseConnectionString(dataType, connectionString)
				.UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
				.UseAutoSyncStructure(true) //自动同步实体结构到数据库
				.UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
				.Build(); //请务必定义成 Singleton 单例模式
			fsql.Aop.AuditValue += (s, e) =>
			{
				if (e.Column.CsType == typeof(DateTime) && e.Property.GetCustomAttribute<LastUpdateTimeAttribute>(false) != null)
				{
					e.Value = DateTime.Now;
				}
				if (e.Column.CsType == typeof(DateTime) && e.Property.GetCustomAttribute<CreateTimeAttribute>(false) != null && e.Value == null)
				{
					e.Value = DateTime.Now;
				}

			};
			return fsql;
		}

		public static IFreeSql Create(string connectionString)
		{
			return Create(DataType.Sqlite, connectionString);
		}

		public static IFreeSql CreateMySql(string connectionString)
		{
			return Create(DataType.MySql, connectionString);
		}

		public static IFreeSql CreatePostgresSql(string connectionString)
		{
			return Create(DataType.PostgreSQL, connectionString);
		}
	}
}
