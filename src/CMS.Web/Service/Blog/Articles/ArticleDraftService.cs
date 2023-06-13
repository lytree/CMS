using System;
using System.Threading.Tasks;
using CMS.Data.Exceptions;
using CMS.Data.Model.Entities.Blog;
using CMS.Data.Repository;
using CMS.Web.Service.Blog.ArticleDrafts;

namespace CMS.Web.Service.Blog.Articles;

public class ArticleDraftService : ApplicationService, IArticleDraftService
{
	private readonly IAuditBaseRepository<ArticleDraftEntity,long> _articleDraftRepository;
	public ArticleDraftService(IAuditBaseRepository<ArticleDraftEntity,long> articleDraftRepository)
	{
		_articleDraftRepository = articleDraftRepository;
	}

	public async Task<ArticleDraftDto> GetAsync(long id)
	{
		ArticleDraftEntity articleDraft = await _articleDraftRepository.Where(r => r.Id == id ).ToOneAsync();
		return Mapper.Map<ArticleDraftDto>(articleDraft);
	}

	public async Task UpdateAsync(long id, UpdateArticleDraftDto updateArticleDto)
	{
		ArticleDraftEntity articleDraft = await _articleDraftRepository.Select.Where(r => r.Id == id).ToOneAsync();
		//if (articleDraft != null && articleDraft.CreateUserId != CurrentUser.FindUserId())
		//{
		//	throw new CMSException("您无权修改他人的随笔");
		//}
		if (articleDraft == null)
		{
			articleDraft = new ArticleDraftEntity { Id = id,  CreateTime = DateTime.Now };
		}
		Mapper.Map(updateArticleDto, articleDraft);
		await _articleDraftRepository.InsertOrUpdateAsync(articleDraft);
	}
}