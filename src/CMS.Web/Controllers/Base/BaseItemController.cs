using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Web.Aop.Filter;
using CMS.Web.Data;
using CMS.Web.Service.Base.BaseItems;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Base;

/// <summary>
/// 数据字典-详情项
/// </summary>
[ApiExplorerSettings(GroupName = "base")]
[Area("base")]
[Route("api/base/item")]
[ApiController]
public class BaseItemController : ControllerBase
{
	private readonly BaseItemService _baseItemService;

	public BaseItemController(BaseItemService baseItemService)
	{
		_baseItemService = baseItemService;
	}

	[HttpDelete("{id}")]
	[CMSAuthorize("删除字典", "字典管理")]
	public async Task<UnifyResponseDto> DeleteAsync(int id)
	{
		await _baseItemService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	[HttpGet]
	public Task<List<BaseItemDto>> GetListAsync([FromQuery] string typeCode)
	{
		return _baseItemService.GetListAsync(typeCode); ;
	}

	[HttpGet("{id}")]
	public Task<BaseItemDto> GetAsync(int id)
	{
		return _baseItemService.GetAsync(id);
	}

	[HttpPost]
	[CMSAuthorize("新增字典", "字典管理")]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUpdateBaseItemDto createBaseItem)
	{
		await _baseItemService.CreateAsync(createBaseItem);
		return UnifyResponseDto.Success("新建字典成功");
	}

	[HttpPut("{id}")]
	[CMSAuthorize("编辑字典", "字典管理")]
	public async Task<UnifyResponseDto> UpdateAsync(int id, [FromBody] CreateUpdateBaseItemDto updateBaseItem)
	{
		await _baseItemService.UpdateAsync(id, updateBaseItem);
		return UnifyResponseDto.Success("更新字典成功");
	}
}