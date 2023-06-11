using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Web.Service.Blog.UserSubscribes;


namespace CMS.Web.Service.Blog.Tags;

public class TagService : ApplicationService, ITagService
{
	private readonly IAuditBaseRepository<UserTag,long> _userTagRepository;
	private readonly IAuditBaseRepository<Tag,long> _tagRepository;
	private readonly IAuditBaseRepository<TagArticle, long> _tagArticleRepository;
	private readonly IFileRepository _fileRepository;
	public TagService(IAuditBaseRepository<Tag,long> tagRepository, IAuditBaseRepository<UserTag,long> userTagRepository, IAuditBaseRepository<TagArticle,long> tagArticleRepository, IFileRepository fileRepository)
	{
		_tagRepository = tagRepository;
		_userTagRepository = userTagRepository;
		_tagArticleRepository = tagArticleRepository;
		_fileRepository = fileRepository;
	}

	public async Task CreateAsync(CreateUpdateTagDto createTag)
	{
		bool exist = await _tagRepository.Select.AnyAsync(r => r.TagName == createTag.TagName);
		if (exist)
		{
			throw new CMSException($"标签[{createTag.TagName}]已存在");
		}

		Tag tag = Mapper.Map<Tag>(createTag);
		await _tagRepository.InsertAsync(tag);
	}

	public async Task UpdateAsync(long id, CreateUpdateTagDto updateTag)
	{
		Tag tag = await _tagRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (tag == null)
		{
			throw new CMSException("该数据不存在");
		}

		bool exist = await _tagRepository.Select.AnyAsync(r => r.TagName == updateTag.TagName && r.Id != id);
		if (exist)
		{
			throw new CMSException($"标签[{updateTag.TagName}]已存在");
		}

		Mapper.Map(updateTag, tag);
		await _tagRepository.UpdateAsync(tag);
	}

	public async Task<TagListDto> GetAsync(long id)
	{
		Tag tag = await _tagRepository.Select.Where(a => a.Id == id).ToOneAsync();
		if (tag == null)
		{
			throw new CMSException("不存在此标签");
		}
		TagListDto tagDto = Mapper.Map<TagListDto>(tag);
		tagDto.IsSubscribe = await IsSubscribeAsync(id);
		tagDto.ThumbnailDisplay = _fileRepository.GetFileUrl(tagDto.Thumbnail);
		return tagDto;
	}

	/// <summary>
	/// 根据状态得到标签列表
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	public async Task<PagedResultDto<TagListDto>> GetListAsync(TagSearchDto searchDto)
	{
		if (searchDto.Sort.IsNullOrEmpty())
		{
			searchDto.Sort = "create_time desc";
		}

		List<TagListDto> tags = (await _tagRepository.Select.IncludeMany(r => r.UserTags, r => r.Where(u => u.CreateUserId == CurrentUser.FindUserId()))
				.WhereIf(searchDto.TagIds.IsNotNullOrEmpty(), r => searchDto.TagIds.Contains(r.Id))
				.WhereIf(searchDto.TagName.IsNotNullOrEmpty(), r => r.TagName.Contains(searchDto.TagName))
				.WhereIf(searchDto.Status != null, r => r.Status == searchDto.Status)
				.OrderBy(searchDto.Sort)
				.ToPagerListAsync(searchDto, out long totalCount))
			.Select(r =>
			{
				TagListDto tagDto = Mapper.Map<TagListDto>(r);
				tagDto.ThumbnailDisplay = _fileRepository.GetFileUrl(tagDto.Thumbnail);
				tagDto.IsSubscribe = r.UserTags.Any();
				return tagDto;
			}).ToList();

		return new PagedResultDto<TagListDto>(tags, totalCount);
	}

	public async Task<bool> IsSubscribeAsync(long tagId)
	{
		if (CurrentUser.FindUserId() == null) return false;
		return await _userTagRepository.Select.AnyAsync(r => r.TagId == tagId && r.CreateUserId == CurrentUser.FindUserId());
	}

	public PagedResultDto<TagListDto> GetSubscribeTags(UserSubscribeSearchDto userSubscribeDto)
	{
		List<long> userTagIds = _userTagRepository.Select
			.Where(u => u.CreateUserId == CurrentUser.FindUserId())
			.ToList(r => r.TagId);

		List<TagListDto> tagListDtos = _userTagRepository.Select.Include(r => r.Tag)
			.Where(r => r.CreateUserId == userSubscribeDto.UserId)
			.OrderByDescending(r => r.CreateTime)
			.ToPagerList(userSubscribeDto, out long count)
			.Select(r =>
			{
				TagListDto tagDto = Mapper.Map<TagListDto>(r.Tag);
				if (tagDto != null)
				{
					tagDto.ThumbnailDisplay = _fileRepository.GetFileUrl(tagDto.Thumbnail);
					tagDto.IsSubscribe = userTagIds.Any(tagId => tagId == tagDto.Id);
				}
				else
				{
					return new TagListDto()
					{
						Id = r.TagId,
						TagName = "该标签已被拉黑",
						IsSubscribe = userTagIds.Any(tagId => tagId == r.TagId)
					};
				}
				return tagDto;
			}).ToList();

		return new PagedResultDto<TagListDto>(tagListDtos, count);
	}

	public async Task UpdateArticleCountAsync(long? id, int inCreaseCount)
	{
		if (id == null)
		{
			return;
		}
		Tag tag = await _tagRepository.Select.Where(r => r.Id == id).ToOneAsync();
		//防止数量一直减，减到小于0
		if (inCreaseCount < 0)
		{
			if (tag.ArticleCount < -inCreaseCount)
			{
				return;
			}
		}
		tag.ArticleCount = tag.ArticleCount + inCreaseCount;

		await _tagRepository.UpdateAsync(tag);
	}


	public async Task UpdateSubscribersCountAsync(long? id, int inCreaseCount)
	{
		if (id == null)
		{
			return;
		}

		Tag tag = await _tagRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (tag == null)
		{
			throw new CMSException("标签不存在", ErrorCode.NotFound);
		}

		tag.UpdateSubscribersCount(inCreaseCount);
		await _tagRepository.UpdateAsync(tag);
	}
	public async Task CorrectedTagCountAsync(long tagId)
	{
		long count = await _tagArticleRepository.Select.Where(r => r.TagId == tagId && r.Article.IsDeleted == false).CountAsync();
		await _tagRepository.UpdateDiy.Set(r => r.ArticleCount, count).Where(r => r.Id == tagId).ExecuteAffrowsAsync();
	}

	public async Task IncreaseTagViewHits(long tagId)
	{
		await _tagRepository.UpdateDiy.Set(r => r.ViewHits + 1)
			.Where(r => r.Id == tagId)
			.ExecuteAffrowsAsync();
	}
}