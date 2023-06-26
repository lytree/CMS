using CMS.Common.Helpers;
using CMS.Common.Extensions;
using CMS.Data.Auth;
using CMS.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CMS.Web.Config;
using CMS.Web.Startup;
using FreeSql;
using CMS.Data.Model.Core;
using CMS.Web.Model.Consts;
using CMS.Web.Tools;

public static class DBServiceCollectionExtensions
{
	/// <summary>
	/// 添加数据库
	/// </summary>
	/// <param name="services"></param>
	/// <param name="env"></param>
	/// <param name="hostAppOptions"></param>
	/// <returns></returns>
	public static void AddDb(this IServiceCollection services, IHostEnvironment env, HostAppOptions hostAppOptions)
	{
		var dbConfig = ConfigHelper.Get<DbConfig>("dbconfig", env.EnvironmentName);
		var appConfig = ConfigHelper.Get<AppConfig>("appconfig", env.EnvironmentName);
		var user = services.BuildServiceProvider().GetService<IUser>();
		//注册数据库
		var idelTime = dbConfig.IdleTime.HasValue && dbConfig.IdleTime.Value > 0 ? TimeSpan.FromMinutes(dbConfig.IdleTime.Value) : TimeSpan.MaxValue;
		//创建数据库
		if (dbConfig.CreateDb)
		{
			DbHelper.CreateDatabaseAsync(dbConfig).Wait();
		}

		var providerType = dbConfig.ProviderType.NotNull() ? Type.GetType(dbConfig.ProviderType) : null;
		var freeSqlBuilder = new FreeSqlBuilder()
				.UseConnectionString(dbConfig.Type, dbConfig.ConnectionString, providerType)
				.UseAutoSyncStructure(false)
				.UseLazyLoading(false)
				.UseNoneCommandParameter(true);

		if (dbConfig.SlaveList?.Length > 0)
		{
			var slaveList = dbConfig.SlaveList.Select(a => a.ConnectionString).ToArray();
			var slaveWeightList = dbConfig.SlaveList.Select(a => a.Weight).ToArray();
			freeSqlBuilder.UseSlave(slaveList).UseSlaveWeight(slaveWeightList);
		}

		hostAppOptions?.ConfigureFreeSqlBuilder?.Invoke(freeSqlBuilder, dbConfig);

		#region 监听所有命令

		if (dbConfig.MonitorCommand)
		{
			freeSqlBuilder.UseMonitorCommand(cmd => { }, (cmd, traceLog) =>
			{
				//Console.WriteLine($"{cmd.CommandText}\n{traceLog}{Environment.NewLine}");
				Console.WriteLine($"{cmd.CommandText}{Environment.NewLine}");
			});
		}

		#endregion 监听所有命令

		var fsql = freeSqlBuilder.Build();

		//生成数据
		if (dbConfig.GenerateData && !dbConfig.CreateDb && !dbConfig.SyncData)
		{
			DbHelper.GenerateDataAsync(fsql, dbConfig).Wait();
		}

		#region 初始化数据库

		if (dbConfig.Type == DataType.Oracle)
		{
			fsql.CodeFirst.IsSyncStructureToUpper = true;
		}

		//同步结构
		if (dbConfig.SyncStructure)
		{
			DbHelper.SyncStructure(fsql, dbConfig: dbConfig);
		}

		#region 审计数据

		//计算服务器时间
		var serverTime = fsql.Ado.QuerySingle(() => DateTime.UtcNow);
		var timeOffset = DateTime.UtcNow.Subtract(serverTime);
		DbHelper.TimeOffset = timeOffset;
		fsql.Aop.AuditValue += (s, e) =>
		{
			DbHelper.AuditValue(e, timeOffset, user);
		};

		#endregion 审计数据

		//同步数据
		if (dbConfig.SyncData)
		{
			DbHelper.SyncDataAsync(fsql, dbConfig).Wait();
		}

		#endregion 初始化数据库

		//软删除过滤器
		fsql.GlobalFilter.ApplyOnly<IDelete>(FilterNames.Delete, a => a.IsDeleted == false);


		hostAppOptions?.ConfigureFreeSql?.Invoke(fsql, dbConfig);

		#region 监听Curd操作

		//if (dbConfig.Curd)
		//{
		//	fsql.Aop.CurdBefore += (s, e) =>
		//	{
		//		if (appConfig.MiniProfiler)
		//		{
		//			MiniProfiler.Current.CustomTiming("CurdBefore", e.Sql);
		//		}
		//		Console.WriteLine($"{e.Sql}{Environment.NewLine}");
		//	};
		//	fsql.Aop.CurdAfter += (s, e) =>
		//	{
		//		if (appConfig.MiniProfiler)
		//		{
		//			MiniProfiler.Current.CustomTiming("CurdAfter", $"{e.ElapsedMilliseconds}");
		//		}
		//	};
		//}

		#endregion 监听Curd操作


		services.AddSingleton<IFreeSql>(fsql);
		services.AddScoped<UnitOfWorkManager>();
		services.AddSingleton(provider => fsql);
	}

	/// <summary>
	/// 添加TiDb数据库
	/// </summary>
	/// <param name="_"></param>
	/// <param name="context"></param>
	/// <param name="version">版本</param>
	public static void AddTiDb(this IServiceCollection _, HostAppContext context, string version = "8.0")
	{
		var dbConfig = ConfigHelper.Get<DbConfig>("dbconfig", context.Environment.EnvironmentName);
		var _dicMySqlVersion = typeof(FreeSqlGlobalExtensions).GetField("_dicMySqlVersion", BindingFlags.NonPublic | BindingFlags.Static);
		var dicMySqlVersion = new ConcurrentDictionary<string, string>();
		dicMySqlVersion[dbConfig.ConnectionString] = version;
		_dicMySqlVersion.SetValue(new ConcurrentDictionary<string, string>(), dicMySqlVersion);
	}
}