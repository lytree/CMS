using System;
using System.Threading.Tasks;
using CMS.Web.Data;


namespace CMS.Web.Service.Blog.Articles;

public interface IArticleService
{
	#region CRUD
	Task<long> CreateAsync(CreateUpdateArticleDto createArticle);

	Task UpdateAsync(long id, CreateUpdateArticleDto updateArticleDto);

	Task<PagedResultDto<ArticleListDto>> GetArticleAsync(ArticleSearchDto searchDto);

	Task DeleteAsync(long id);

	Task<ArticleDto> GetAsync(long id);
	#endregion

	///// <summary>
	///// 得到我关注的人发布的随笔
	///// </summary>
	///// <param name="pageDto"></param>
	///// <returns></returns>
	//Task<PagedResultDto<ArticleListDto>> GetSubscribeArticleAsync(PageDto pageDto);

	/// <summary>
	/// 更新随笔点赞量
	/// </summary>
	/// <param name="subjectId"></param>
	/// <param name="likesQuantity"></param>
	/// <returns></returns>
	Task UpdateLikeQuantityAysnc(long subjectId, int likesQuantity);

	/// <summary>
	/// 修改随笔是否允许其他人评论
	/// </summary>
	/// <param name="id"></param>
	/// <param name="commentable"></param>
	/// <returns></returns>
	Task UpdateCommentable(long id, bool commentable);
}