using System;
using System.Threading.Tasks;
using CMS.Web.Data;
using CMS.Web.Service.Blog.Tags;
using CMS.Web.Service.Blog.UserSubscribes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 用户关注标签
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/user-tag")]
[ApiController]
[Authorize]
public class UserTagController : ControllerBase
{
	private readonly ITagService _tagService;
	private readonly IUserTagService _userTagService;

	public UserTagController(ITagService tagService, IUserTagService userTagService)
	{
		_userTagService = userTagService;
		_tagService = tagService;
	}

	/// <summary>
	/// 用户关注标签
	/// </summary>
	/// <param name="tagId"></param>
	[HttpPost("{tagId}")]
	public Task CreateUserTagAsync(long tagId)
	{
		return _userTagService.CreateUserTagAsync(tagId);
	}

	/// <summary>
	/// 取消关注标签
	/// </summary>
	/// <param name="tagId"></param>
	[HttpDelete("{tagId}")]
	public Task DeleteUserTagAsync(long tagId)
	{
		return _userTagService.DeleteUserTagAsync(tagId);
	}

	/// <summary>
	/// 用户关注的标签的分页数据
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[AllowAnonymous]
	public PagedResultDto<TagListDto> GetUserTagList([FromQuery] UserSubscribeSearchDto userSubscribeDto)
	{
		return _tagService.GetSubscribeTags(userSubscribeDto);
	}
}