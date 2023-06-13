
using LinCms.Aop.Filter;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using CMS.Web.Service.Blog;
using CMS.Data.Repository;
using CMS.Data.Model.Entities.Blog;
using CMS.Web.Service.Blog.Tags;
using CMS.Web.Data;
using CMS.Web.Aop.Filter;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 标签
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/tags")]
[ApiController]
public class TagController : ControllerBase
{
	private readonly IAuditBaseRepository<TagEntity,long> _tagRepository;
	private readonly ITagService _tagService;
	public TagController(IAuditBaseRepository<TagEntity,long> tagRepository, ITagService tagService)
	{
		_tagRepository = tagRepository;
		_tagService = tagService;
	}

	[HttpDelete("{id}")]
	[CMSAuthorize("删除标签", "标签管理")]
	public async Task<UnifyResponseDto> DeleteAsync(long id)
	{
		await _tagRepository.DeleteAsync(new TagEntity { Id = id });
		return UnifyResponseDto.Success();
	}

	[HttpGet]
	[CMSAuthorize("所有标签", "标签管理")]
	public async Task<PagedResultDto<TagListDto>> GetAllAsync([FromQuery] TagSearchDto searchDto)
	{
		return await _tagService.GetListAsync(searchDto);
	}

	[HttpGet("public")]
	public virtual async Task<PagedResultDto<TagListDto>> GetListAsync([FromQuery] TagSearchDto searchDto)
	{
		searchDto.Status = true;
		return await _tagService.GetListAsync(searchDto);
	}

	[HttpGet("{id}")]
	public async Task<TagListDto> GetAsync(long id)
	{
		await _tagService.IncreaseTagViewHits(id);
		return await _tagService.GetAsync(id);
	}

	[HttpPost]
	[CMSAuthorize("新增标签", "标签管理")]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateUpdateTagDto createTag)
	{
		await _tagService.CreateAsync(createTag);
		return UnifyResponseDto.Success("新建标签成功");
	}

	[CMSAuthorize("编辑标签", "标签管理")]
	[HttpPut("{id}")]
	public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] CreateUpdateTagDto updateTag)
	{
		await _tagService.UpdateAsync(id, updateTag);
		return UnifyResponseDto.Success("更新标签成功");
	}

	/// <summary>
	/// 标签-校正标签对应随笔数量
	/// </summary>
	/// <param name="tagId"></param>
	/// <returns></returns>
	[CMSAuthorize("校正随笔数量", "标签管理")]
	[HttpPut("correct/{tagId}")]
	public async Task<UnifyResponseDto> CorrectedTagCountAsync(long tagId)
	{
		await _tagService.CorrectedTagCountAsync(tagId);
		return UnifyResponseDto.Success();
	}
}