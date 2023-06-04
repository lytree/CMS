using CMS.Data.Repository;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
	public static class ConfigFreeRepository
	{
		public static void AddDataRepository(this IServiceCollection services, string connect)
		{
			IFreeSql fsql = new FreeSqlBuilder()
				.UseConnectionString(DataType.MySql, connect)
				.UseAutoSyncStructure(true) //自动迁移实体的结构到数据库
				.Build(); //请务必定义成 Singleton 单例模式
			services.AddSingleton<IFreeSql>(fsql);
			services.AddFreeRepository();
		}
	}
}
