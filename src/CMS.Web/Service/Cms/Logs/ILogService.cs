using CMS.Data.Model.Entities.Other;
using CMS.Web.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CMS.Web.Service.Cms.Logs;

public interface ILogService
{
	Task CreateAsync(LinLog linlog);
	PagedResultDto<LinLog> GetUserLogs(LogSearchDto searchDto);

	List<string> GetLoggedUsers(PageDto searchDto);

	/// <summary>
	/// 管理端访问与用户统计
	/// </summary>
	/// <returns></returns>
	VisitLogUserDto GetUserAndVisits();
}