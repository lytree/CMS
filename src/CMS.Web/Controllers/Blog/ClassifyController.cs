using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Web.Aop.Filter;
using CMS.Web.Data;
using CMS.Web.Service.Blog.Classifies;


using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 分类专栏
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/classifies")]
[ApiController]
public class ClassifyController : ControllerBase
{
	private readonly IClassifyService _classifyService;

	public ClassifyController(IClassifyService classifyService)
	{
		_classifyService = classifyService;
	}

	[HttpDelete("{id}")]
	public async Task<UnifyResponseDto> DeleteClassify(long id)
	{
		await _classifyService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	[HttpGet]
	public List<ClassifyDto> GetListByUserId(long? userId)
	{
		return _classifyService.GetListByUserId(userId);
	}

	[CMSAuthorize("删除", "分类专栏")]
	[HttpDelete("cms/{id}")]
	public async Task<UnifyResponseDto> Delete(long id)
	{
		await _classifyService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	[CMSAuthorize("分类专栏列表", "分类专栏")]
	[HttpGet("cms")]
	public Task<PagedResultDto<ClassifyDto>> GetListAsync([FromQuery] ClassifySearchDto pageDto)
	{
		return _classifyService.GetListAsync(pageDto);
	}

	[HttpGet("{id}")]
	public Task<ClassifyDto> GetAsync(long id)
	{
		return _classifyService.GetAsync(id);
	}

	[HttpPost]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUpdateClassifyDto createClassify)
	{
		await _classifyService.CreateAsync(createClassify);
		return UnifyResponseDto.Success("新建分类专栏成功");
	}

	[HttpPut("{id}")]
	public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] CreateUpdateClassifyDto updateClassify)
	{
		await _classifyService.UpdateAsync(id, updateClassify);
		return UnifyResponseDto.Success("更新分类专栏成功");
	}
}