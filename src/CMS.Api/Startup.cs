using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CMS.Api.Controllers;
using CMS.Api.Extensions;
using CMS.Api.Filters;
using CMS.Data;
using CMS.Service;
using Serilog.Events;
using Serilog;

namespace CMS.Api;

static class Startup
{

	/// <summary>
	/// 创建host
	/// </summary>
	/// <param name="options"></param>
	/// <returns></returns>
	public static void CreateWebApplication(this WebApplicationBuilder builder)
	{

		builder.ConfigureHost();
		builder.ConfigureWebHost();
		builder.ConfigureConfiguration();
		builder.ConfigureServices();
		builder.ConfigureLogger();
	}
	/// <summary>
	/// 配置通用主机
	/// </summary>
	/// <param name="builder"></param>
	public static void ConfigureHost(this WebApplicationBuilder builder)
	{
		// Add services to the container.
		builder.Host.UseSystemd().UseWindowsService();
	}

	/// <summary>
	/// 配置web主机
	/// </summary>
	/// <param name="builder"></param>
	public static void ConfigureWebHost(this WebApplicationBuilder builder)
	{
		builder.WebHost.UseShutdownTimeout(TimeSpan.FromSeconds(1d));
	}


	/// <summary>
	/// 配置配置
	/// </summary>
	/// <param name="builder"></param>
	public static void ConfigureConfiguration(this WebApplicationBuilder builder)
	{
		const string APPSETTINGS = "appsettings";
		if (Directory.Exists(APPSETTINGS) == true)
		{
			foreach (var file in Directory.GetFiles(APPSETTINGS, "appsettings.*.json"))
			{
				var jsonFile = Path.Combine(APPSETTINGS, Path.GetFileName(file));
				builder.Configuration.AddJsonFile(jsonFile, true, true);
			}
		}
	}


	/// <summary>
	/// 配置服务
	/// </summary>
	/// <param name="builder"></param>
	[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Dictionary<string, dynamic>))]
	public static void ConfigureServices(this WebApplicationBuilder builder)
	{
		var services = builder.Services;
		var configuration = builder.Configuration;
		services.Configure<AppOptions>(configuration);
		services.AddControllers(options =>
		{
			options.Filters.Add<ResultFilterAttribute>();

		});
		var conn = configuration.GetConnectionString("MySQL");

		services.AddDataRepository(conn);
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(option =>
		{

		});
		services.AddAutoMapper(option =>
		{
			option.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
		});
		services.AddHostedService<HostService>();


		services.AddScoped<LinksService>().
			AddScoped<LinksController>();
	}

	/// <summary>
	/// 配置应用
	/// </summary>
	/// <param name="app"></param>
	public static void ConfigureApp(this WebApplication app)
	{
		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();
	}
	/// <summary>
	/// 配置应用
	/// </summary>
	/// <param name="app"></param>
	public static void ConfigureLogger(this WebApplicationBuilder build)
	{
		var flushInterval = new TimeSpan(0, 0, 1);
		var file = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath), "App.log");
		var loggerConfig = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File(file, flushToDiskInterval: flushInterval, encoding: System.Text.Encoding.UTF8, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 22).CreateLogger();
		build.Logging.AddSerilog(loggerConfig);
		build.Services.AddSingleton<LoggerFactory>();

	}
}
