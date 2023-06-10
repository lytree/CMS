using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.Web.Service.Cms.Groups;
using LinCms.Aop.Filter;
using LinCms.Data;
using LinCms.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Cms;

/// <summary>
/// 分组
/// </summary>
[ApiExplorerSettings(GroupName = "cms")]
[Route("cms/admin/group")]
[ApiController]
public class GroupController : ControllerBase
{
	private readonly IGroupService _groupService;
	public GroupController(IGroupService groupService)
	{
		_groupService = groupService;
	}

	[HttpGet("all")]
	[CMSAuthorize("查询所有权限组", "管理员")]
	public Task<List<LinGroup>> GetListAsync()
	{
		return _groupService.GetListAsync();
	}

	[HttpGet("{id}")]
	[CMSAuthorize("查询一个权限组及其权限", "管理员")]
	public async Task<GroupDto> GetAsync(long id)
	{
		GroupDto groupDto = await _groupService.GetAsync(id);
		return groupDto;
	}

	[HttpPost]
	[CMSAuthorize("新建权限组", "管理员")]
	public async Task<UnifyResponseDto> CreateAsync([FromBody] CreateGroupDto inputDto)
	{
		await _groupService.CreateAsync(inputDto);
		return UnifyResponseDto.Success("新建分组成功");
	}

	[HttpPut("{id}")]
	[CMSAuthorize("更新一个权限组", "管理员")]
	public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] UpdateGroupDto updateGroupDto)
	{
		await _groupService.UpdateAsync(id, updateGroupDto);
		return UnifyResponseDto.Success("更新分组成功");
	}

	[HttpDelete("{id}")]
	[CMSAuthorize("删除一个权限组", "管理员")]
	public async Task<UnifyResponseDto> DeleteAsync(long id)
	{
		await _groupService.DeleteAsync(id);
		return UnifyResponseDto.Success("删除分组成功");
	}

}