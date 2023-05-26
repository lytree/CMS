using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using static PInvoke.AdvApi32;
namespace CMS.Platform.Windows;

/// <summary>
/// IHostBuilder扩展
/// </summary>
static class ServiceExtensions
{
	/// <summary>
	/// 控制命令
	/// </summary>
	private enum Command
	{
		Start,
		Stop,
	}

	[SupportedOSPlatform("linux")]
	[DllImport("libc", SetLastError = true)]
	private static extern uint geteuid();

	/// <summary>
	/// 使用windows服务
	/// </summary>
	/// <param name="hostBuilder"></param> 
	/// <returns></returns>
	public static IHostBuilder UseWindowsService(this IHostBuilder hostBuilder)
	{
		return WindowsServiceLifetimeHostBuilderExtensions.UseWindowsService(hostBuilder);
	}

	/// <summary>
	/// 运行主机
	/// </summary>
	/// <param name="app"></param>
	/// <param name="singleton"></param>
	public static void Run(this WebApplication app, bool singleton)
	{
		var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(CMS));
		if (UseCommand(logger) == false)
		{
			using var mutex = new Mutex(true, "Global\\FastGithub", out var firstInstance);
			if (singleton == false || firstInstance)
			{
				app.Run();
			}
			else
			{
				logger.LogWarning($"程序将自动关闭：系统已运行其它实例");
			}
		}
	}

	/// <summary>
	/// 使用命令
	/// </summary>
	/// <param name="logger"></param>
	/// <returns></returns>
	private static bool UseCommand(ILogger logger)
	{
		var args = Environment.GetCommandLineArgs();
		if (Enum.TryParse<Command>(args.Skip(1).FirstOrDefault(), true, out var cmd) == false)
		{
			return false;
		}

		var action = cmd == Command.Start ? "启动" : "停止";
		try
		{
			if (OperatingSystem.IsWindows())
			{
				UseCommandAtWindows(cmd);
			}
			else
			{
				return false;
			}
			logger.LogInformation($"服务{action}成功");
		}
		catch (Exception ex)
		{
			logger.LogError(ex.Message, $"服务{action}异常");
		}
		return true;
	}

	/// <summary>
	/// 应用控制指令
	/// </summary> 
	/// <param name="cmd"></param>
	[SupportedOSPlatform("windows")]
	private static void UseCommandAtWindows(Command cmd)
	{
		var binaryPath = Environment.GetCommandLineArgs().First();
		var serviceName = Path.GetFileNameWithoutExtension(binaryPath);
		var state = true;
		if (cmd == Command.Start)
		{
			state =InstallAndStartService(serviceName, binaryPath);
		}
		else if (cmd == Command.Stop)
		{
			state = StopAndDeleteService(serviceName);
		}

		if (state == false)
		{
			throw new Win32Exception();
		}
	}
		/// <summary>
	/// 安装并启动服务
	/// </summary>
	/// <param name="serviceName"></param>
	/// <param name="binaryPath"></param>
	/// <param name="startType"></param>
	/// <returns></returns>
	[SupportedOSPlatform("windows")]
	public static bool InstallAndStartService(string serviceName, string binaryPath, ServiceStartType startType = ServiceStartType.SERVICE_AUTO_START)
	{
		using var hSCManager = OpenSCManager(null, null, ServiceManagerAccess.SC_MANAGER_ALL_ACCESS);
		if (hSCManager.IsInvalid == true)
		{
			return false;
		}

		var hService = OpenService(hSCManager, serviceName, ServiceAccess.SERVICE_ALL_ACCESS);
		if (hService.IsInvalid == true)
		{
			hService = CreateService(
				hSCManager,
				serviceName,
				serviceName,
				ServiceAccess.SERVICE_ALL_ACCESS,
				ServiceType.SERVICE_WIN32_OWN_PROCESS,
				startType,
				ServiceErrorControl.SERVICE_ERROR_NORMAL,
				Path.GetFullPath(binaryPath),
				lpLoadOrderGroup: null,
				lpdwTagId: 0,
				lpDependencies: null,
				lpServiceStartName: null,
				lpPassword: null);
		}

		if (hService.IsInvalid == true)
		{
			return false;
		}

		using (hService)
		{
			return StartService(hService, 0, null);
		}
	}

	/// <summary>
	/// 停止并删除服务
	/// </summary>
	/// <param name="serviceName"></param>
	/// <returns></returns>
	[SupportedOSPlatform("windows")]
	public static bool StopAndDeleteService(string serviceName)
	{
		using var hSCManager = OpenSCManager(null, null, ServiceManagerAccess.SC_MANAGER_ALL_ACCESS);
		if (hSCManager.IsInvalid == true)
		{
			return false;
		}

		using var hService = OpenService(hSCManager, serviceName, ServiceAccess.SERVICE_ALL_ACCESS);
		if (hService.IsInvalid == true)
		{
			return true;
		}

		var status = new SERVICE_STATUS();
		if (QueryServiceStatus(hService, ref status) == true)
		{
			if (status.dwCurrentState != ServiceState.SERVICE_STOP_PENDING &&
				status.dwCurrentState != ServiceState.SERVICE_STOPPED)
			{
				ControlService(hService, ServiceControl.SERVICE_CONTROL_STOP, ref status);
			}
		}

		return DeleteService(hService);
	}
}