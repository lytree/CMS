using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
	public static class ConfigureFreeSql
	{
		public static void AddFreeSql(this IServiceCollection services, IConfiguration configuration)
		{
			var freeSql = FreeSqlFactory.Create(configuration.GetConnectionString("SQLite"));

			services.AddSingleton(freeSql);

			// 仓储模式支持
			services.AddFreeRepository();
		}
	}
}
