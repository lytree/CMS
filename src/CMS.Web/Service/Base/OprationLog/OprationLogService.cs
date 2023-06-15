using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CMS.Web.Service.Base.OprationLog.Dto;
using CMS.Web.Model.Dto;
using CMS.DynamicApi;
using CMS.DynamicApi.Attributes;
using CMS.Web.Model.Consts;
using CMS.Data.Repository.OprationLog;
using Microsoft.AspNetCore.Mvc;
using CMS.Common.Extensions;
using CMS.Common.Helpers;
using CMS.Data.Model.Entities.Base;

namespace CMS.Web.Service.Base.OprationLog;

/// <summary>
/// 操作日志服务
/// </summary>
[Order(200)]
[DynamicApi(Area = AdminConsts.AreaName)]
public class OprationLogService : BaseService, IOprationLogService, IDynamicApi
{
	private readonly IHttpContextAccessor _context;
	private readonly IOprationLogRepository _oprationLogRepository;

	public OprationLogService(
		IHttpContextAccessor context,
		IOprationLogRepository oprationLogRepository
	)
	{
		_context = context;
		_oprationLogRepository = oprationLogRepository;
	}

	/// <summary>
	/// 查询分页
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<PageOutput<OprationLogListOutput>> GetPageAsync(PageInput<LogGetPageDto> input)
	{
		var userName = input.Filter?.CreatedUserName;

		var list = await _oprationLogRepository.Select
		.WhereDynamicFilter(input.DynamicFilter)
		.WhereIf(userName.NotNull(), a => a.CreatedUserName.Contains(userName))
		.Count(out var total)
		.OrderByDescending(true, c => c.Id)
		.Page(input.CurrentPage, input.PageSize)
		.ToListAsync<OprationLogListOutput>();

		var data = new PageOutput<OprationLogListOutput>()
		{
			List = list,
			Total = total
		};

		return data;
	}

	/// <summary>
	/// 新增
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public async Task<long> AddAsync(OprationLogAddInput input)
	{
		string ua = _context.HttpContext.Request.Headers["User-Agent"];
		if (ua.NotNull())
		{
			var client = UAParser.Parser.GetDefault().Parse(ua);
			var device = client.Device.Family;
			device = device.ToLower() == "other" ? "" : device;
			input.Browser = client.UA.Family;
			input.Os = client.OS.Family;
			input.Device = device;
			input.BrowserInfo = ua;
		}

		input.Name = User.Name;
		input.IP = IPHelper.GetIP(_context?.HttpContext?.Request);

		var entity = Mapper.Map<OprationLogEntity>(input);
		await _oprationLogRepository.InsertAsync(entity);

		return entity.Id;
	}
}