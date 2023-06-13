using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Web.Service.Blog.Articles;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Data;
using CMS.Data.Exceptions;
using CMS.Web.Aop.Filter;
using CMS.Data.Extensions;

namespace CMS.Web.Controllers.Blog;

/// <summary>
/// 随笔
/// </summary>
[ApiExplorerSettings(GroupName = "blog")]
[Area("blog")]
[Route("api/blog/articles")]
[ApiController]
[Authorize]
public class ArticleController : ControllerBase
{
	private readonly IAuditBaseRepository<ArticleEntity, long> _articleRepository;
	private readonly IArticleService _articleService;
	private readonly IMapper _mapper;

	public ArticleController(IAuditBaseRepository<ArticleEntity, long> articleRepository, IMapper mapper, IArticleService articleService)
	{
		_articleRepository = articleRepository;
		_mapper = mapper;
		_articleService = articleService;
	}

	/// <summary>
	/// 删除自己的随笔
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpDelete("{id}")]
	public async Task<UnifyResponseDto> DeleteMyArticleAsync(long id)
	{
		bool isCreateArticle = await _articleRepository.Select.AnyAsync(r => r.Id == id);
		if (!isCreateArticle)
		{
			throw new CMSException("无法删除别人的随笔!");
		}

		await _articleService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	/// <summary>
	/// 管理员删除违规随笔
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpDelete("cms/{id}")]
	[CMSAuthorize("删除随笔", "随笔")]
	public async Task<UnifyResponseDto> Delete(long id)
	{
		await _articleService.DeleteAsync(id);
		return UnifyResponseDto.Success();
	}

	/// <summary>
	/// 我所有的随笔
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	[HttpGet]
	[AllowAnonymous]
	public PagedResultDto<ArticleListDto> Get([FromQuery] ArticleSearchDto searchDto)
	{
		List<ArticleListDto> articles = _articleRepository
			.Select
			.IncludeMany(r => r.Tags, r => r.Where(u => u.Status == true))
			.WhereIf(searchDto.Title.IsNotNullOrEmpty(), r => r.Title.Contains(searchDto.Title))
			.WhereIf(searchDto.ClassifyId.HasValue, r => r.ClassifyId == searchDto.ClassifyId)
			.OrderByDescending(r => r.IsStickie)
			.OrderByDescending(r => r.Id)
			.ToPagerList(searchDto, out long totalCount)
			.ConvertAll(a => _mapper.Map<ArticleListDto>(a))
;

		return new PagedResultDto<ArticleListDto>(articles, totalCount);
	}

	/// <summary>
	/// 得到所有已审核过的随笔,最新的随笔/三天、七天、月榜、全部
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	[HttpGet("query")]
	[AllowAnonymous]
	public Task<PagedResultDto<ArticleListDto>> GetArticleAsync([FromQuery] ArticleSearchDto searchDto)
	{
		return _articleService.GetArticleAsync(searchDto);

		//string redisKey = "article:query:" + EncryptUtil.Encrypt(JsonConvert.SerializeObject(searchDto, Formatting.Indented, new JsonSerializerSettings
		//{
		//    DefaultValueHandling = DefaultValueHandling.Ignore
		//}));

		//return RedisHelper.CacheShellAsync(
		//    redisKey, 60, () => _articleService.GetArticleAsync(searchDto)
		// );
	}

	/// <summary>
	/// 得到所有的随笔
	/// </summary>
	/// <param name="searchDto"></param>
	/// <returns></returns>
	[HttpGet("all")]
	[CMSAuthorize("所有随笔", "随笔")]
	public PagedResultDto<ArticleListDto> GetAllArticles([FromQuery] ArticleSearchDto searchDto)
	{
		var articles = _articleRepository
			.Select
			.WhereCascade(exp: r => r.IsDeleted == false)
			.WhereIf(searchDto.Title.IsNotNullOrEmpty(), r => r.Title.Contains(searchDto.Title))
			.OrderByDescending(r => r.CreateTime)
			.ToPagerList(searchDto, out long totalCount)
			.ConvertAll(a => _mapper.Map<ArticleListDto>(a))
;

		return new PagedResultDto<ArticleListDto>(articles, totalCount);
	}

	/// <summary>
	/// 随笔详情
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id}")]
	[AllowAnonymous]
	public async Task<ArticleDto> GetAsync(long id)
	{
		await _articleRepository.UpdateDiy.Set(r => r.ViewHits + 1).Where(r => r.Id == id).ExecuteAffrowsAsync();
		return await _articleService.GetAsync(id);
	}

	[HttpPost]
	public async Task<long> CreateAsync([FromBody] CreateUpdateArticleDto createArticle)
	{
		long id = await _articleService.CreateAsync(createArticle);
		return id;
	}
	[HttpPut("{id}")]
	public async Task<UnifyResponseDto> UpdateAsync(long id, [FromBody] CreateUpdateArticleDto updateArticleDto)
	{

		await _articleService.UpdateAsync(id, updateArticleDto);
		return UnifyResponseDto.Success("更新随笔成功");
	}

	/// <summary>
	/// 审核随笔-拉黑/取消拉黑
	/// </summary>
	/// <param name="id">审论Id</param>
	/// <param name="isAudit"></param>
	/// <returns></returns>
	[CMSAuthorize("审核随笔", "随笔")]
	[HttpPut("audit/{id}")]
	public async Task<UnifyResponseDto> AuditAsync(long id, bool isAudit)
	{
		ArticleEntity article = await _articleRepository.Select.Where(r => r.Id == id).ToOneAsync();
		if (article == null)
		{
			throw new CMSException("没有找到相关随笔");
		}

		article.IsAudit = isAudit;
		await _articleRepository.UpdateAsync(article);
		return UnifyResponseDto.Success();
	}

	///// <summary>
	///// 得到我关注的人 发布的随笔
	///// </summary>
	///// <param name="pageDto"></param>
	///// <returns></returns>
	//[HttpGet("subscribe")]
	//public Task<PagedResultDto<ArticleListDto>> GetSubscribeArticleAsync([FromQuery] PageDto pageDto)
	//{
	//	return _articleService.GetSubscribeArticleAsync(pageDto);
	//}

	/// <summary>
	/// 修改随笔 是否允许其他人评论
	/// </summary>
	/// <param name="id">随笔主键</param>
	/// <param name="commentable">true:允许评论;false:不允许评论</param>
	/// <returns></returns>
	[HttpPut("{id}/comment-able/{commentable}")]
	public Task UpdateCommentable(long id, bool commentable)
	{
		return _articleService.UpdateCommentable(id, commentable);
	}
}