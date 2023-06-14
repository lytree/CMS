using System.Threading.Tasks;
using ZhonTai.Admin.Domain;
using CMS.Web.Service.Base.OprationLog.Dto;
using CMS.Web.Model.Dto;

namespace CMS.Web.Service.Base.OprationLog;

/// <summary>
/// 操作日志接口
/// </summary>
public interface IOprationLogService
{
	Task<PageOutput<OprationLogListOutput>> GetPageAsync(PageInput<LogGetPageDto> input);

	Task<long> AddAsync(OprationLogAddInput input);
}