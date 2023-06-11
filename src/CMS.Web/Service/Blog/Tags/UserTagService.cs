using System;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Service;


namespace CMS.Web.Service.Blog.Tags;

public class UserTagService : ApplicationService, IUserTagService
{
	private readonly IAuditBaseRepository<Tag,long> _tagRepository;
	private readonly IAuditBaseRepository<UserTag,long> _userTagRepository;
	private readonly ITagService _tagService;

	public UserTagService(ITagService tagService, IAuditBaseRepository<Tag,long> tagRepository, IAuditBaseRepository<UserTag,long> userTagRepository)
	{
		_tagService = tagService;
		_tagRepository = tagRepository;
		_userTagRepository = userTagRepository;
	}

	public async Task CreateUserTagAsync(long tagId)
	{
		Tag tag = await _tagRepository.Select.Where(r => r.Id == tagId).ToOneAsync();
		if (tag == null)
		{
			throw new CMSException("该标签不存在");
		}

		if (!tag.Status)
		{
			throw new CMSException("该标签已被拉黑");
		}

		bool any = await _userTagRepository.Select.AnyAsync(r => r.CreateUserId == CurrentUser.FindUserId() && r.TagId == tagId);
		if (any)
		{
			throw new CMSException("您已关注该标签");
		}

		UserTag userTag = new() { TagId = tagId };
		await _userTagRepository.InsertAsync(userTag);
		await _tagService.UpdateSubscribersCountAsync(tagId, 1);
	}

	public async Task DeleteUserTagAsync(long tagId)
	{
		bool any = await _userTagRepository.Select.AnyAsync(r => r.CreateUserId == CurrentUser.FindUserId() && r.TagId == tagId);
		if (!any)
		{
			throw new CMSException("已取消关注");
		}
		await _userTagRepository.DeleteAsync(r => r.TagId == tagId && r.CreateUserId == CurrentUser.FindUserId());
		await _tagService.UpdateSubscribersCountAsync(tagId, -1);
	}
}