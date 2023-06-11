using System;
using System.Threading.Tasks;
using CMS.Web.Data;
using CMS.Web.Service.Blog.UserSubscribes;


namespace CMS.Web.Service.Blog.Tags;

public interface ITagService
{
	Task CreateAsync(CreateUpdateTagDto createTag);

	Task UpdateAsync(long id, CreateUpdateTagDto updateTag);

	Task<TagListDto> GetAsync(long id);

	Task<PagedResultDto<TagListDto>> GetListAsync(TagSearchDto searchDto);

	/// <summary>
	/// 判断标签是否被关注
	/// </summary>
	/// <param name="tagId"></param>
	/// <returns></returns>
	Task<bool> IsSubscribeAsync(long tagId);

	/// <summary>
	/// 得到某个用户关注的标签
	/// </summary>
	/// <param name="userSubscribeDto"></param>
	/// <returns></returns>
	PagedResultDto<TagListDto> GetSubscribeTags(UserSubscribeSearchDto userSubscribeDto);

	Task UpdateArticleCountAsync(long? id, int inCreaseCount);

	Task UpdateSubscribersCountAsync(long? id, int inCreaseCount);

	Task CorrectedTagCountAsync(long tagId);

	/// <summary>
	/// 标签浏览量+1
	/// </summary>
	/// <param name="tagId"></param>
	/// <returns></returns>
	Task IncreaseTagViewHits(long tagId);
}