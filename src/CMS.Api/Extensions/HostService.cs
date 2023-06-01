using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;

namespace CMS.Api.Extensions;

public class HostService : BackgroundService
{
    private readonly IHost host;
    private readonly IOptions<AppOptions> appOptions;
    private readonly ILogger<HostService> logger;

    public HostService(IHost host, IOptions<AppOptions> appOptions, ILogger<HostService> logger)
    {
        this.host = host;
        this.appOptions = appOptions;
        this.logger = logger ?? NullLogger<HostService>.Instance;
    }

    /// <summary>
    /// 启动完成
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("启动完毕");
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// 后台任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1d), stoppingToken);
        await WaitForParentProcessExitAsync(stoppingToken);
    }

    /// <summary>
    /// 等待父进程退出
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task WaitForParentProcessExitAsync(CancellationToken cancellationToken)
    {
        var parentId = appOptions.Value.ParentProcessId;
        if (parentId <= 0)
        {
            return;
        }

        try
        {
            Process.GetProcessById(parentId).WaitForExit();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"获取进程{parentId}异常");
        }
        finally
        {
            logger.LogInformation($"正在主动关闭，因为父进程已退出");
            await host.StopAsync(cancellationToken);
        }
    }

}
