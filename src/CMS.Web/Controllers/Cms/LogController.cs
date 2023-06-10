using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Web.Service.Cms.Logs;
using IGeekFan.FreeKit.Extras.FreeSql;
using LinCms.Aop.Filter;
using LinCms.Data;
using LinCms.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Cms;

/// <summary>
/// 日志
/// </summary>
[ApiExplorerSettings(GroupName = "cms")]
[Route("cms/log")]
[ApiController]
[DisableAuditing]
public class LogController : ControllerBase
{
	private readonly ILogService _logService;
	private readonly ISerilogService _serilogService;

	public LogController(ILogService logService, ISerilogService serilogService)
	{
		_logService = logService;
		_serilogService = serilogService;
	}

	/// <summary>
	/// 查询日志记录的用户
	/// </summary>
	/// <returns></returns>
	[HttpGet("users")]
	[CMSAuthorize("查询日志记录的用户", "日志")]
	public List<string> GetUsers([FromQuery] PageDto pageDto)
	{
		return _logService.GetLoggedUsers(pageDto);
	}

	/// <summary>
	/// 日志浏览（人员，时间），分页展示
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[CMSAuthorize("查询所有日志", "日志")]
	public PagedResultDto<LinLog> GetLogs([FromQuery] LogSearchDto searchDto)
	{
		return _logService.GetUserLogs(searchDto);
	}

	/// <summary>
	/// 日志搜素（人员，时间）（内容）， 分页展示
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	[HttpGet("search")]
	[CMSAuthorize("搜索日志", "日志")]
	public PagedResultDto<LinLog> GetUserLogs([FromQuery] LogSearchDto searchDto)
	{
		return _logService.GetUserLogs(searchDto);
	}

	/// <summary>
	/// Serilog日志
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	[HttpGet("serilog")]
	[CMSAuthorize("Serilog日志", "日志")]
	public Task<PagedResultDto<SerilogDO>> GetSerilogListAsync([FromQuery] SerilogSearchDto searchDto)
	{
		return _serilogService.GetListAsync(searchDto);
	}

	[HttpGet("visitis")]
	public VisitLogUserDto GetUserAndVisits()
	{
		return _logService.GetUserAndVisits();
	}

	[HttpGet("dashboard")]
	public Task<LogDashboard> GetLogDashboard()
	{
		return _serilogService.GetLogDashboard();
	}
}