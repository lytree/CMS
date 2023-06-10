using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Web.Aop.Filter;
using CMS.Web.Data;
using CMS.Web.Service.Base.BaseTypes;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Base;

/// <summary>
/// 数据字典-分类
/// </summary>
[ApiExplorerSettings(GroupName = "base")]
[Area("base")]
[Route("api/base/type")]
[ApiController]
public class BaseTypeController : ControllerBase
{
	private readonly IBaseTypeService _baseTypeService;
	public BaseTypeController(IBaseTypeService baseTypeService)
	{
		_baseTypeService = baseTypeService;
	}

	[HttpDelete("{id}")]
	[CMSAuthorize("删除字典类别", "字典类别")]
	public async Task<UnifyResponseDto> DeleteAsync(int id)
	{
		await _baseTypeService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	[HttpGet]
	public Task<List<BaseTypeDto>> GetListAsync()
	{
		return _baseTypeService.GetListAsync();
	}

	[HttpGet("{id}")]
	public Task<BaseTypeDto> GetAsync(int id)
	{
		return _baseTypeService.GetAsync(id);
	}

	[HttpPost]
	[CMSAuthorize("新增字典类别", "字典类别")]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUpdateBaseTypeDto createBaseType)
	{
		await _baseTypeService.CreateAsync(createBaseType);
		return UnifyResponseDto.Success("新建类别成功");
	}

	[HttpPut("{id}")]
	[CMSAuthorize("编辑字典类别", "字典类别")]
	public async Task<UnifyResponseDto> UpdateAsync(int id, [FromBody] CreateUpdateBaseTypeDto updateBaseType)
	{
		await _baseTypeService.UpdateAsync(id, updateBaseType);
		return UnifyResponseDto.Success("更新类别成功");
	}
}