using System.Threading.Tasks;
using ZhonTai.Admin.Domain;
using CMS.Web.Service.User.LoginLog.Dto;
using CMS.Web.Model.Dto;

namespace CMS.Web.Service.User.LoginLog;

/// <summary>
/// 登录日志接口
/// </summary>
public interface ILoginLogService
{
	Task<PageOutput<LoginLogListOutput>> GetPageAsync(PageInput<LogGetPageDto> input);

	Task<long> AddAsync(LoginLogAddInput input);
}