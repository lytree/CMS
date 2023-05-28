
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
			IFreeSql fsql = new FreeSqlBuilder()
				.UseConnectionString(dataType, connectionString)
				.UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
				.UseAutoSyncStructure(true) //自动同步实体结构到数据库
				.UseMonitorCommand(cmd => Trace.WriteLine(cmd.CommandText))
				.Build(); //请务必定义成 Singleton 单例模式
			return fsql;
		}

		public static IFreeSql Create(string connectionString)
		{
			return Create(DataType.Sqlite, connectionString);
		}
	}
}
