using System.Threading.Tasks;
using CMS.Data.Model.Entities;
using CMS.Web.Data;


namespace CMS.Web.Service.Cms.Logs;

public interface ISerilogService
{
	Task<LogDashboard> GetLogDashboard();
	Task<PagedResultDto<SerilogDO>> GetListAsync(SerilogSearchDto searchDto);
}