using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using static PInvoke.AdvApi32;

namespace CMS.Api;

static class ServiceExtensions
{
	/// <summary>
	/// 控制命令
	/// </summary>
	enum Command
	{
		Start,
		Stop,
	}
	/// <summary>
	/// 使用命令
	/// </summary>
	/// <param name="logger"></param>
	/// <returns></returns>
	internal static bool UseCommand(ILogger logger)
	{
		var args = Environment.GetCommandLineArgs();
		if (!Enum.TryParse<Command>(args.Skip(1).FirstOrDefault(), true, out var cmd))
		{
			return false;
		}

		var action = cmd == Command.Start ? "启动" : "停止";
		try
		{
			if (OperatingSystem.IsLinux())
			{
				UseCommandAtLinux(cmd);
			}
			else if (OperatingSystem.IsWindows())
			{
				UseCommandAtWin(cmd);
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
	[SupportedOSPlatform("linux")]
	[DllImport("libc", SetLastError = true)]
	private static extern uint geteuid();


	/// <summary>
	/// 应用控制指令
	/// </summary> 
	/// <param name="cmd"></param>
	[SupportedOSPlatform("linux")]
	static void UseCommandAtLinux(Command cmd)
	{
		if (geteuid() != 0)
		{
			throw new UnauthorizedAccessException("无法操作服务：没有root权限");
		}

		var binaryPath = Path.GetFullPath(Environment.GetCommandLineArgs().First());
		var serviceName = Path.GetFileNameWithoutExtension(binaryPath);
		var serviceFilePath = $"/etc/systemd/system/{serviceName}.service";

		if (cmd == Command.Start)
		{
			var serviceBuilder = new StringBuilder()
				.AppendLine("[Unit]")
				.AppendLine($"Description={serviceName}")
				.AppendLine()
				.AppendLine("[Service]")
				.AppendLine("Type=notify")
				.AppendLine($"User={Environment.UserName}")
				.AppendLine($"ExecStart={binaryPath}")
				.AppendLine($"WorkingDirectory={Path.GetDirectoryName(binaryPath)}")
				.AppendLine()
				.AppendLine("[Install]")
				.AppendLine("WantedBy=multi-user.target");
			File.WriteAllText(serviceFilePath, serviceBuilder.ToString());

			Process.Start("chcon", $"--type=bin_t {binaryPath}").WaitForExit(); // SELinux
			Process.Start("systemctl", "daemon-reload").WaitForExit();
			Process.Start("systemctl", $"start {serviceName}.service").WaitForExit();
			Process.Start("systemctl", $"enable {serviceName}.service").WaitForExit();
		}
		else if (cmd == Command.Stop)
		{
			Process.Start("systemctl", $"stop {serviceName}.service").WaitForExit();
			Process.Start("systemctl", $"disable {serviceName}.service").WaitForExit();

			if (File.Exists(serviceFilePath))
			{
				File.Delete(serviceFilePath);
			}
			Process.Start("systemctl", "daemon-reload").WaitForExit();
		}
	}



	/// <summary>
	/// 应用控制指令
	/// </summary> 
	/// <param name="cmd"></param>
	[SupportedOSPlatform("windows")]
	private static void UseCommandAtWin(Command cmd)
	{
		var binaryPath = Environment.GetCommandLineArgs().First();
		var serviceName = Path.GetFileNameWithoutExtension(binaryPath);
		var state = true;
		if (cmd == Command.Start)
		{
			state = InstallAndStartService(serviceName, binaryPath);
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
	private static bool InstallAndStartService(string serviceName, string binaryPath, ServiceStartType startType = ServiceStartType.SERVICE_AUTO_START)
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
	private static bool StopAndDeleteService(string serviceName)
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
